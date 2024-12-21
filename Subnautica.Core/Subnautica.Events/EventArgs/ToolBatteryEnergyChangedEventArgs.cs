namespace Subnautica.Events.EventArgs
{
    using System;

    public class ToolBatteryEnergyChangedEventArgs : EventArgs
    {
        public ToolBatteryEnergyChangedEventArgs(string uniqueId, global::Pickupable item)
        {
            this.UniqueId = uniqueId;
            this.Item     = item;
        }

        public string UniqueId { get; private set; }

        public global::Pickupable Item { get; private set; }
    }
}
