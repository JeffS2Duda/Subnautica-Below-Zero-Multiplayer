namespace Subnautica.Events.EventArgs
{
    using Subnautica.API.Features;
    using System;

    public class EnergyMixinClickingEventArgs : EventArgs
    {
        public EnergyMixinClickingEventArgs(string uniqueId, string batterySlotId, TechType techType, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.BatterySlotId = batterySlotId.Replace(ZeroGame.GetVehicleBatteryLabelUniqueId(null, true), "");
            this.TechType = techType;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public string BatterySlotId { get; set; }

        public TechType TechType { get; set; }

        public bool IsAllowed { get; set; }
    }
}
