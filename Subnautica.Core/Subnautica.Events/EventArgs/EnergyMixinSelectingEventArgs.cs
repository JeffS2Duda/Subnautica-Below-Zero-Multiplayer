namespace Subnautica.Events.EventArgs
{
    using System;

    public class EnergyMixinSelectingEventArgs : EventArgs
    {
        public EnergyMixinSelectingEventArgs(string uniqueId, string batterySlotId, TechType batteryType, TechType techType, Pickupable item, bool isAdding = false, bool isChanging = false, bool isAllowed = true)
        {
            this.UniqueId      = uniqueId;
            this.BatterySlotId = batterySlotId;
            this.BatteryType   = batteryType;
            this.TechType      = techType;
            this.Item          = item;
            this.IsAdding      = isAdding;
            this.IsChanging    = isChanging;
            this.IsAllowed     = isAllowed;
        }

        public string UniqueId { get; set; }

        public string BatterySlotId { get; set; }

        public TechType TechType { get; set; }

        public TechType BatteryType { get; set; }

        public Pickupable Item { get; set; }

        public bool IsAdding { get; set; }

        public bool IsChanging { get; set; }

        public bool IsAllowed { get; set; }
    }
}
