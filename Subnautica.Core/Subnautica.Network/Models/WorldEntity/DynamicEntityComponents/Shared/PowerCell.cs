namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared
{
    using MessagePack;

    using UnityEngine;

    [MessagePackObject]
    public class PowerCell
    {
        [Key(0)]
        public string UniqueId { get; set; }

        [Key(1)]
        public float Charge { get; set; } = 200f;

        [Key(2)]
        public float Capacity { get; set; } = 200f;

        [Key(3)]
        public TechType TechType { get; set; } = TechType.PowerCell;

        [IgnoreMember]
        public bool IsFull
        {
            get
            {
                return this.Charge == this.Capacity;
            }
        }

        [IgnoreMember]
        public bool IsExists
        {
            get
            {
                return this.Charge != -1f;
            }
        }

        public bool AddEnergy(float energyAmount, out float usedEnergyAmount)
        {
            if (this.IsFull || !this.IsExists)
            {
                usedEnergyAmount = 0f;
                return false;
            }

            usedEnergyAmount = Mathf.Min(this.Capacity - this.Charge, energyAmount);

            this.Charge += usedEnergyAmount;
            return true;
        }

        public bool ConsumeEnergy(float energyAmount, out float usedEnergyAmount)
        {
            usedEnergyAmount = 0f;

            if (this.Charge > 0f && energyAmount > 0f)
            {
                var oldCharge = this.Charge;

                this.Charge = Mathf.Max(0f, oldCharge - energyAmount);

                usedEnergyAmount = oldCharge - this.Charge;
                return true;
            }

            return false;
        }

        public void SetBatteryType(TechType techType)
        {
            this.TechType = techType;
            this.Capacity = techType == TechType.PrecursorIonPowerCell ? 1000f : 200f;
        }
    }
}
