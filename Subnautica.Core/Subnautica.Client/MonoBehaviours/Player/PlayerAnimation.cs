namespace Subnautica.Client.MonoBehaviours.Player
{
    using RootMotion.FinalIK;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Structures;

    using UnityEngine;

    using static RootMotion.FinalIK.IKSolver;

    public class PlayerAnimation : MonoBehaviour
    {
        public void Awake()
        {
            this.Animator = this.gameObject.GetComponent<Animator>();
            this.RigidBody = this.gameObject.GetComponent<Rigidbody>();
            this.RigidBody.mass = global::Player.main.rigidBody.mass;
            this.RigidBody.useGravity = false;
            this.RigidBody.SetInterpolation(RigidbodyInterpolation.None);
            this.RigidBody.SetKinematic();

            foreach (var component in this.GetComponentsInChildren<AimIK>())
            {
                if (component.solver.transform.name.Contains("R_aim"))
                {
                    this.RightArmBone = component.solver.bones[0];
                }
                else
                {
                    this.LeftArmBone = component.solver.bones[0];
                }
            }
        }

        public void Update()
        {
            if (World.IsLoaded && this.Player != null && this.Player.IsCreatedModel)
            {
                this.IsTeleported = false;

                this.InterpolateCameraViewPitch();

                SafeAnimator.SetBool(this.Animator, "cinematics_enabled", !VRGameOptions.GetVrAnimationMode());
                SafeAnimator.SetBool(this.Animator, "holding_tool", this.Player.TechTypeInHand != TechType.None && this.Player.TechTypeInHand != TechType.PDA);
                SafeAnimator.SetBool(this.Animator, "holding_loot", this.Player.TechTypeInHand == TechType.PDA);
                SafeAnimator.SetBool(this.Animator, "holding_welder", this.Player.TechTypeInHand == TechType.Welder);
                SafeAnimator.SetBool(this.Animator, "in_hovercraft", this.Player.VehicleType == TechType.Hoverbike);
                SafeAnimator.SetBool(this.Animator, "in_exosuit", this.Player.VehicleType == TechType.Exosuit);
                SafeAnimator.SetBool(this.Animator, "piloting_seatruck", this.Player.VehicleType == TechType.SeaTruck);
                SafeAnimator.SetBool(this.Animator, "on_surface", this.Player.IsOnSurface);
                SafeAnimator.SetBool(this.Animator, "is_underwater", this.Player.IsUnderwater || this.Player.IsInWaterPark);
                SafeAnimator.SetBool(this.Animator, "is_floating", this.Player.IsPrecursorArm);
                SafeAnimator.SetFloat(this.Animator, "view_pitch", this.CurrentCameraPitch);

                if (this.Player.IsMovementActive && !this.Player.IsCinematicModeActive)
                {
                    if (this.Player.VehicleType == TechType.None)
                    {
                        this.InterpolateRotation();
                        this.InterpolateMovement(this.Player.IsInSeaTruck);
                    }

                    this.UpdateMovementAnimation();
                    this.UpdatePlayerAnimations();
                }
                else
                {
                    this.Player.SetVelocity(Vector3.zero);
                }

                this.UpdatePlayerEmotes();
            }
        }

        public void LateUpdate()
        {
            if (World.IsLoaded && this.Player != null && this.Player.IsCreatedModel)
            {
                if (this.Player.TechTypeInHand != TechType.None && this.Player.TechTypeInHand != TechType.QuantumLocker && this.Player.TechTypeInHand != TechType.Seaglide && this.Player.TechTypeInHand != TechType.TeleportationTool)
                {
                    if (this.CurrentLeftArmRotation == Quaternion.identity)
                    {
                        this.CurrentLeftArmRotation = this.LeftArmBone.transform.localRotation;
                        this.CurrentRightArmRotation = this.RightArmBone.transform.localRotation;
                    }

                    this.RightArmBone.transform.localRotation = this.CurrentRightArmRotation = BroadcastInterval.QuaternionSmoothDamp(this.CurrentRightArmRotation, this.Player.RightHandItemRotation, ref this.RightHandItemVelocity, 0.1f);
                    this.LeftArmBone.transform.localRotation = this.CurrentLeftArmRotation = BroadcastInterval.QuaternionSmoothDamp(this.CurrentLeftArmRotation, this.Player.LeftHandItemRotation, ref this.LeftHandItemVelocity, 0.1f);
                }
                else
                {
                    this.CurrentRightArmRotation = this.CurrentLeftArmRotation = Quaternion.identity;
                }
            }
        }

        public void FixedUpdate()
        {
            if (this.Player != null && this.Player.IsCreatedModel)
            {
                this.UpdateMovementState();
                this.UpdateIsInSeaTruck();
                this.UpdateIsUnderwater();
                this.UpdateIsOnSurface();
            }
        }

        public void UpdateMovementAnimation()
        {
            if (this.IsTeleported || this.Player.VehicleType != TechType.None)
            {
                this.SmoothedVelocity = Vector3.zero;
            }
            else
            {
                var relativeVelocity = this.RigidBody.transform.InverseTransformDirection(this.SmoothedPositionVelocity - this.RigidBody.GetPointVelocity(this.RigidBody.position));

                this.SmoothedVelocity = Vector3.Slerp(this.SmoothedVelocity, relativeVelocity, ((this.Player.IsUnderwater || this.Player.IsInWaterPark) ? 1f : 4f) * Time.deltaTime);
            }

            this.Animator.SetFloat(AnimatorHashID.move_speed, this.SmoothedVelocity.magnitude);
            this.Animator.SetFloat(AnimatorHashID.move_speed_x, this.SmoothedVelocity.x);
            this.Animator.SetFloat(AnimatorHashID.move_speed_y, this.SmoothedVelocity.y);
            this.Animator.SetFloat(AnimatorHashID.move_speed_z, this.SmoothedVelocity.z);
        }

        public void UpdatePlayerAnimations()
        {
            if (this.Player.AnimationQueue.Count > 0)
            {
                foreach (var animation in this.Player.AnimationQueue.Dequeue())
                {
                    SafeAnimator.SetBool(this.Animator, animation.Key, animation.Value);
                }
            }
        }

        public void UpdateMovementState()
        {
            if (this.Player.IsInSeaTruck && Client.Multiplayer.Furnitures.Bed.IsSleeping(this.Player.UniqueId))
            {
                this.Player.DisableMovement();
            }
            else
            {
                this.Player.EnableMovement();
            }
        }

        public bool UpdateIsInSeaTruck(bool force = false, bool isKeepPosition = false)
        {
            if (!force && (this.LastInteriorId == this.Player.CurrentInteriorId || !this.Player.IsMovementActive))
            {
                return false;
            }

            if (this.Player.CurrentInteriorId.IsNull())
            {
                this.LastInteriorId = this.Player.CurrentInteriorId;

                if (this.Player.IsInSeaTruck)
                {
                    this.transform.parent = null;
                    this.Player.Position = this.transform.position;
                    this.Player.IsInSeaTruck = false;
                }

                return true;
            }

            var seaTruckSegment = Network.Identifier.GetComponentByGameObject<global::SeaTruckSegment>(this.Player.CurrentInteriorId, true);
            if (seaTruckSegment != null)
            {
                this.LastInteriorId = this.Player.CurrentInteriorId;
                this.transform.parent = seaTruckSegment.transform;
                this.Player.IsInSeaTruck = true;

                if (isKeepPosition)
                {
                    this.transform.localPosition = this.Player.Position = seaTruckSegment.transform.InverseTransformPoint(this.transform.position);
                }

                return true;
            }

            return false;
        }

        private void UpdateIsUnderwater()
        {
            if (this.Player.CurrentSubRootId.IsNotNull())
            {
                var subRoot = Network.Identifier.GetComponentByGameObject<SubRoot>(this.Player.CurrentSubRootId);
                if (subRoot)
                {
                    this.Player.IsUnderwater = subRoot.IsUnderwater(this.Player.Position);
                }
                else
                {
                    this.Player.IsUnderwater = false;
                }
            }
            else if (this.Player.CurrentInteriorId.IsNotNull() || this.Player.VehicleType != TechType.None)
            {
                this.Player.IsUnderwater = false;
            }
            else
            {
                this.Player.IsUnderwater = this.transform.position.y < Ocean.GetOceanLevel();
            }
        }

        private void UpdateIsOnSurface()
        {
            var y = this.transform.position.y;
            if (y > Ocean.GetOceanLevel() - 1.0 && y < Ocean.GetOceanLevel() + 1.0 && this.Player.VehicleType == TechType.None && this.Player.CurrentInteriorId.IsNull() && this.Player.CurrentSubRootId.IsNull())
            {
                this.Player.IsOnSurface = true;
            }
            else
            {
                this.Player.IsOnSurface = false;
            }
        }

        public void InterpolateMovement(bool isInSeaTruck)
        {
            if (isInSeaTruck)
            {
                if (ZeroVector3.Distance(this.Player.Position, transform.localPosition) > 100f)
                {
                    this.IsTeleported = true;

                    transform.localPosition = this.Player.Position;
                }
                else
                {
                    transform.localPosition = Vector3.SmoothDamp(transform.localPosition, this.Player.Position, ref this.SmoothedPositionVelocity, 0.1f);
                }
            }
            else
            {
                if (ZeroVector3.Distance(this.Player.Position, transform.position) > 100f)
                {
                    this.IsTeleported = true;

                    transform.position = this.Player.Position;
                }
                else
                {
                    transform.position = Vector3.SmoothDamp(transform.position, this.Player.Position, ref this.SmoothedPositionVelocity, 0.1f);
                }
            }

            this.Player.SetVelocity(this.SmoothedPositionVelocity);
        }

        public void InterpolateRotation()
        {
            this.transform.rotation = BroadcastInterval.QuaternionSmoothDamp(this.transform.rotation, this.Player.Rotation, ref this.SmoothedRotationVelocity, 0.1f);
        }

        private void InterpolateCameraViewPitch()
        {
            this.CurrentCameraPitch = Mathf.SmoothDamp(this.CurrentCameraPitch, this.Player.CameraPitch, ref this.CameraPitchVelocity, 0.1f);
        }

        private bool UpdatePlayerEmotes()
        {
            if (this.Player.EmoteIndex == this.CurrentEmoteIndex)
            {
                return false;
            }

            if (this.Player.EmoteIndex == 0.0f)
            {
                this.CurrentEmoteIndex = 0.0f;

                this.Animator.SetFloat("FP_Emotes", 0.0f);
            }
            else
            {
                if (this.Player.IsCinematicModeActive || this.Player.VehicleType != TechType.None || this.Player.TechTypeInHand != TechType.None)
                {
                    return false;
                }

                this.CurrentEmoteIndex = this.Player.EmoteIndex;

                this.Animator.Rebind();
                this.Animator.SetFloat("FP_Emotes", this.Player.EmoteIndex);
            }

            return true;
        }

        public Vector3 GetVelocity()
        {
            return this.SmoothedPositionVelocity;
        }

        public ZeroPlayer Player;

        public Bone LeftArmBone;

        public Bone RightArmBone;

        private Animator Animator;

        private Rigidbody RigidBody;

        private Vector3 SmoothedVelocity = Vector3.zero;

        private Vector3 SmoothedPositionVelocity = Vector3.zero;

        private Quaternion SmoothedRotationVelocity;

        private Quaternion RightHandItemVelocity;

        private Quaternion LeftHandItemVelocity;

        private Quaternion CurrentRightArmRotation;

        private Quaternion CurrentLeftArmRotation;

        private float CurrentCameraPitch;

        private float CameraPitchVelocity;

        private string LastInteriorId;

        private float CurrentEmoteIndex;

        private bool IsTeleported;
    }
}
