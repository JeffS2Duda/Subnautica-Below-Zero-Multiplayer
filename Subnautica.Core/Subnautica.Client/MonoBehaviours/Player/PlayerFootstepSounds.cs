namespace Subnautica.Client.MonoBehaviours.Player
{
    using Subnautica.API.Features;
    using Subnautica.Network.Structures;

    using UnityEngine;

    public class PlayerFootstepSounds : MonoBehaviour
    {
        public global::FootstepSounds FootstepSounds_Player;

        public global::FootstepSounds FootstepSounds_Exosuit;

        public global::FootstepSounds CurrentFootstepSounds;

        public PlayerAnimation PlayerAnimation;

        public PlayerVehicleManagement PlayerVehicle;

        public ZeroPlayer Player;

        public float CurrentVelocity = 0f;

        public bool IsUnderwater = false;

        public float MaxFootstepRange = 25f;

        public void Awake()
        {
            this.PlayerAnimation = this.gameObject.GetComponent<PlayerAnimation>();
            this.PlayerVehicle = this.gameObject.GetComponent<PlayerVehicleManagement>();

            this.FootstepSounds_Player = this.gameObject.AddComponent<global::FootstepSounds>();
            this.FootstepSounds_Exosuit = this.gameObject.AddComponent<global::FootstepSounds>();

            this.FootstepSounds_Player.CancelInvoke("TriggerSounds");
            this.FootstepSounds_Exosuit.CancelInvoke("TriggerSounds");

            this.FootstepSounds_Player.enabled = false;
            this.FootstepSounds_Exosuit.enabled = false;

            this.FootstepSounds_Player.soundsEnabled = false;
            this.FootstepSounds_Exosuit.soundsEnabled = false;

            this.RefreshPlayerSettings();

            this.InvokeRepeating("TriggerMultiplayerSounds", 0f, 0.05f);
        }

        public void TriggerMultiplayerSounds()
        {
            this.UpdatePlayerDatas();

            if (this.ShouldPlayStepSounds())
            {
                this.CurrentFootstepSounds = this.GetFootstepSounds();

                var magnitude = this.CurrentVelocity;
                var timeDiff = Time.time - this.CurrentFootstepSounds.timeLastFootStep;

                if (this.CurrentFootstepSounds.clampedSpeed > 0f)
                {
                    magnitude = Mathf.Min(this.CurrentFootstepSounds.clampedSpeed, magnitude);
                }

                var requiredTime = 2.5f * this.CurrentFootstepSounds.footStepFrequencyMod / magnitude;
                if (timeDiff >= requiredTime)
                {
                    this.OnStep();

                    this.CurrentFootstepSounds.timeLastFootStep = Time.time;
                    this.CurrentFootstepSounds.triggeredLeft = !this.CurrentFootstepSounds.triggeredLeft;
                }
            }
        }

        public bool ShouldPlayStepSounds()
        {
            if (this.enabled == false)
            {
                return false;
            }

            if (ZeroVector3.Distance(this.transform.position, global::Player.main.transform.position) > this.MaxFootstepRange * this.MaxFootstepRange)
            {
                return false;
            }

            if (this.Player.VehicleType == TechType.None)
            {
                if (this.Player.CurrentSurfaceType == VFXSurfaceTypes.none)
                {
                    return false;
                }

                if (!this.FootstepSounds_Player.soundsEnabled)
                {
                    return false;
                }

                if (this.Player.IsUnderwater)
                {
                    return false;
                }

                return this.CurrentVelocity > 0.2f;
            }

            if (this.Player.VehicleType == TechType.Exosuit)
            {
                this.RefreshExosuitSettings();

                if (!this.FootstepSounds_Exosuit.soundsEnabled || this.PlayerVehicle.Vehicle == null)
                {
                    return false;
                }

                var exosuit = this.PlayerVehicle.Vehicle.GetComponent<global::Exosuit>();
                if (exosuit == null || !exosuit.mainAnimator.GetBool("onGround"))
                {
                    return false;
                }

                return this.CurrentVelocity > 0.2f;
            }

            return false;
        }

        private void OnStep()
        {
            FakeFMODByBenson.Instance.PlaySound(this.CurrentFootstepSounds.footStepSound, this.transform, this.MaxFootstepRange, this.OnStepParameters);
        }

        private void OnStepParameters(FMOD.Studio.EventInstance eventInstance)
        {
            eventInstance.setParameterValueByIndex(this.CurrentFootstepSounds.speedParamIndex, this.CurrentVelocity);
            eventInstance.setParameterValueByIndex(this.CurrentFootstepSounds.surfaceParamIndex, this.Player.VehicleType == TechType.Exosuit ? (float)VFXSurfaceTypes.metal : (float)this.Player.CurrentSurfaceType);
            eventInstance.setParameterValueByIndex(this.CurrentFootstepSounds.inWaterParamIndex, this.IsUnderwater ? 1f : 0f);
            eventInstance.setParameterValue("wormlair", this.CurrentFootstepSounds.iceWormAmbience);
        }

        private bool RefreshPlayerSettings()
        {
            this.FootstepSounds_Player = this.CopyFootstepSettings(global::Player.main.footStepSounds, this.FootstepSounds_Player);
            return true;
        }

        private bool RefreshExosuitSettings()
        {
            if (this.FootstepSounds_Exosuit.soundsEnabled)
            {
                return true;
            }

            var entity = Network.DynamicEntity.GetEntity(this.Player.VehicleId);
            if (entity == null)
            {
                return false;
            }

            entity.UpdateGameObject();

            if (entity.GameObject && entity.GameObject.TryGetComponent<global::FootstepSounds>(out var footstepSounds))
            {
                this.FootstepSounds_Exosuit = this.CopyFootstepSettings(footstepSounds, this.FootstepSounds_Exosuit);
                return true;
            }

            return false;
        }

        private FootstepSounds GetFootstepSounds()
        {
            return this.Player.VehicleType == TechType.Exosuit ? this.FootstepSounds_Exosuit : this.FootstepSounds_Player;
        }

        private FootstepSounds CopyFootstepSettings(FootstepSounds fromFootstepSounds, FootstepSounds toFootstepSounds)
        {
            toFootstepSounds.footStepSound = fromFootstepSounds.footStepSound;
            toFootstepSounds.speedParamIndex = fromFootstepSounds.speedParamIndex;
            toFootstepSounds.surfaceParamIndex = fromFootstepSounds.surfaceParamIndex;
            toFootstepSounds.inWaterParamIndex = fromFootstepSounds.inWaterParamIndex;

            toFootstepSounds.clampedSpeed = fromFootstepSounds.clampedSpeed;
            toFootstepSounds.footStepFrequencyMod = fromFootstepSounds.footStepFrequencyMod;

            toFootstepSounds.soundsEnabled = true;
            return toFootstepSounds;
        }

        private void UpdatePlayerDatas()
        {
            this.IsUnderwater = false;
            this.CurrentVelocity = 0f;

            if (this.Player.VehicleType == TechType.Exosuit)
            {
                var vehicle = this.PlayerVehicle.GetCurrentVehicle();
                if (vehicle != null)
                {
                    this.CurrentVelocity = vehicle.GetVelocity().magnitude;

                    var exosuit = this.PlayerVehicle.Vehicle.GetComponent<global::Exosuit>();
                    if (exosuit)
                    {
                        this.IsUnderwater = exosuit.IsUnderwater();
                    }
                }
            }
            else
            {
                this.CurrentVelocity = this.PlayerAnimation.GetVelocity().magnitude;
            }
        }
    }
}