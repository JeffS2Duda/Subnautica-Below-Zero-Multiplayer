namespace Subnautica.Events.EventArgs
{
    using System;

    public class VehicleInteriorToggleEventArgs : EventArgs
    {
        public VehicleInteriorToggleEventArgs(string uniqueId, bool isEnter)
        {
            this.UniqueId  = uniqueId;
            this.IsEnter   = isEnter;
        }

        public string UniqueId { get; private set; }

        public bool IsEnter { get; private set; }
    }
}
