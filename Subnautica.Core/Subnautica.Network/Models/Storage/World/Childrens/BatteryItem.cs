namespace Subnautica.Network.Models.Storage.World.Childrens
{
    using System;

    using MessagePack;

    [MessagePackObject]
    public class BatteryItem
    {
        [Key(0)]
        public bool IsActive { get; set; } = false;

        [Key(1)]
        public string SlotId { get; set; }

        [Key(2)]
        public TechType TechType { get; set; }

        [Key(3)]
        public float Charge { get; set; }

        [Key(4)]
        public float Capacity { get; set; } = 100f;

        public BatteryItem()
        {

        }

        public BatteryItem(string slotId = null, TechType techType = TechType.None, float charge = 0f, float capacity = 100f)
        {
            this.SlotId   = slotId;
            this.TechType = techType;
            this.Charge   = charge;
            this.Capacity = capacity;
        }

        public byte GetSlotId()
        {
            switch (this.TechType)
            {
                case TechType.Battery:
                case TechType.PrecursorIonBattery:
                    return Byte.Parse(this.SlotId.Replace("BatteryCharger", ""));

                case TechType.PowerCell:
                case TechType.PrecursorIonPowerCell:
                    return Byte.Parse(this.SlotId.Replace("PowerCellCharger", ""));
            }

            return 0;
        }

        public void SetBattery(TechType techType, float charge)
        {
            this.Clear();

            if (techType != TechType.None)
            {
                this.IsActive = true;
                this.TechType = techType;
                this.Charge   = charge;
                this.Capacity = this.GetCapacity();
            }
        }

        public float GetCapacity()
        {
            switch (this.TechType)
            {
                case TechType.Battery:
                    return 100f;
                case TechType.PowerCell:
                    return 200f;
                case TechType.PrecursorIonBattery:
                    return 500f;
                case TechType.PrecursorIonPowerCell:
                    return 1000f;
            }

            return 100f;
        }

        public void Clear()
        {
            this.IsActive = false;
            this.TechType = TechType.None;
            this.Charge   = 0f;
            this.Capacity = 0f;
        }
    }
}
