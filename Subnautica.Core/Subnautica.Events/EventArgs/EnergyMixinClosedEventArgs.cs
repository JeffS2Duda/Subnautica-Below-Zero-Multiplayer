namespace Subnautica.Events.EventArgs
{
    using System;

    public class EnergyMixinClosedEventArgs : EventArgs
    {
        public EnergyMixinClosedEventArgs(string uniqueId, string batterySlotId, TechType techType)
        {
            this.UniqueId = uniqueId;
            this.BatterySlotId = batterySlotId;
            this.TechType = techType;
        }

        public string UniqueId { get; set; }

        public string BatterySlotId { get; set; }

        public TechType TechType { get; set; }
    }
}
