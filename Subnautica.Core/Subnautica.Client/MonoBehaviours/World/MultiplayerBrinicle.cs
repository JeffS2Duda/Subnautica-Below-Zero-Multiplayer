namespace Subnautica.Client.MonoBehaviours.World
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using UnityEngine;

    public class MultiplayerBrinicle : MonoBehaviour
    {
        private global::Brinicle Brinicle { get; set; }

        private string UniqueId { get; set; }

        private float ScaleAmount { get; set; }

        private bool IsActive { get; set; } = true;

        public void Awake()
        {
            if (this.TryGetComponent<global::Brinicle>(out var brinicle))
            {
                this.Brinicle = brinicle;
                this.UniqueId = this.gameObject.GetIdentityId();

                this.CheckBrinicleState(true);
            }
        }

        public void Update()
        {
            if (this.IsActive && !this.IsScaleComplete())
            {
                var brinicle = this.GetBrinicle(this.UniqueId);
                if (brinicle != null)
                {
                    var scaleAmount = brinicle.GetScaleAmount(Network.Session.GetWorldTime());
                    if (this.ScaleAmount != scaleAmount)
                    {
                        this.SetScaleAmount(scaleAmount);
                    }
                }
            }
        }

        public void FixedUpdate()
        {
            this.CheckBrinicleState();

            if (this.IsActive)
            {
                this.Brinicle.UpdatePlayerDamage(Time.time);
            }
        }

        private void CheckBrinicleState(bool force = false)
        {
            var brinicle = this.GetBrinicle(this.UniqueId);
            if (brinicle == null)
            {
                this.SetState(false);
            }
            else
            {
                var isActive = brinicle.IsActive(Network.Session.GetWorldTime());
                if (this.IsActive != isActive || force)
                {
                    this.SetState(isActive, brinicle);
                }
            }
        }

        private void SetState(bool isActive, Brinicle brinicle = null)
        {
            this.IsActive = isActive;
            this.Brinicle.state = isActive ? global::Brinicle.State.Enabled : global::Brinicle.State.Disabled;
            this.Brinicle.model.gameObject.SetActive(isActive);

            if (isActive)
            {
                this.Brinicle.fxController?.Play(0);
                this.Brinicle.liveMixin.health = brinicle.LiveMixin.Health;
                this.Brinicle.transform.localEulerAngles = brinicle.EularAngles.ToVector3();
                this.Brinicle.model.localScale = brinicle.FullScale.ToVector3();
                this.Brinicle.fullScale = brinicle.FullScale.ToVector3();
            }
            else
            {

                if (this.Brinicle.breakAfterFadeOut && this.Brinicle.fxController)
                {
                    this.Brinicle.fxController.Play(1);
                }

                this.Brinicle.fxController?.Stop(0);
                this.Brinicle.UnfreezeAll();
            }

            this.SetScaleAmount(0f);
        }

        private void SetScaleAmount(float amount)
        {
            this.ScaleAmount = amount;

            this.Brinicle.model.localScale = Vector3.Lerp(this.Brinicle.zeroScale, this.Brinicle.fullScale, amount);

            if (this.ScaleAmount >= 1f)
            {
                this.ScaleAmount = 99f;
            }
        }

        private bool IsScaleComplete()
        {
            return this.ScaleAmount >= 99f;
        }

        private Brinicle GetBrinicle(string uniqueId)
        {
            return Network.Session.GetBrinicle(uniqueId);
        }
    }
}
