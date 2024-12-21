namespace Subnautica.Network.Models.Storage.World.Childrens
{
    using MessagePack;

    using UnityEngine;

    [MessagePackObject]
    public class PowerSource
    {
        [Key(0)]
        public uint ConstructionId { get; set; }

        [Key(1)]
        public float Power { get; set; }

        [Key(2)]
        public float MaxPower { get; set; }

        [Key(3)]
        public float ConsumedEnergy { get; set; }

        public void ModifyPower(float energyAmount)
        {
            this.Power = Mathf.Clamp(this.Power + energyAmount, 0.0f, this.MaxPower);
        }

        public void ModifyConsumedEnergy(float consumedEnergy)
        {
            this.ConsumedEnergy += consumedEnergy;
        }

        public void SetConsumedEnergy(float energy)
        {
            this.ConsumedEnergy = energy;
        }
    }
}
