namespace Subnautica.Client.MonoBehaviours.Construction
{
    using System;

    using Subnautica.API.Features;
    using UnityEngine;

    using Constructing = Subnautica.Client.Multiplayer.Constructing;

    public class BuilderNormalAnimation : MonoBehaviour
    {
        public Constructing.Builder Builder;

        private float TargetConstructedAmount { get; set; } = 0.0f;

        private float ConstructionLeftTime { get; set; } = 0.0f;

        public bool IsActive { get; set; } = false;

        public void Update()
        {
            if (this.IsActive && this.Builder != null && this.Builder.Constructable != null)
            {
                this.UpdateChangedAmount();
            }
        }

        public void SetTargetConstructedAmount(float targetAmount)
        {
            this.TargetConstructedAmount = targetAmount;
            this.ConstructionLeftTime = BroadcastInterval.ConstructingAmountChanged;
            this.IsActive = true;
        }

        private void UpdateChangedAmount()
        {
            float differentAmount = this.GetDifferentAmount();
            if (differentAmount >= 0 && this.GetConstructedAmount() + differentAmount >= this.TargetConstructedAmount) 
            {
                this.Builder.Constructable.constructedAmount = this.TargetConstructedAmount;
                this.IsActive = false;
            }
            else if (differentAmount < 0 && this.GetConstructedAmount() - differentAmount <= this.TargetConstructedAmount)
            {
                this.Builder.Constructable.constructedAmount = this.TargetConstructedAmount;
                this.IsActive = false;
            }
            else
            {
                this.Builder.Constructable.constructedAmount += differentAmount;
            }

            this.Builder.Constructable.UpdateMaterial();
        }

        private float GetDifferentAmount()
        {
            float differentAmount  = (this.TargetConstructedAmount - this.GetConstructedAmount()) / (this.ConstructionLeftTime / Time.deltaTime);
            this.ConstructionLeftTime -= Time.deltaTime;

            return differentAmount;
        }

        private float GetConstructedAmount()
        {
            return (float) Math.Round(this.Builder.Constructable.constructedAmount, 4);
        }
    }
}
