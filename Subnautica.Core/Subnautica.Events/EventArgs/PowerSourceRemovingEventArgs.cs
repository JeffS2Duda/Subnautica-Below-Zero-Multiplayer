namespace Subnautica.Events.EventArgs
{
    using System;

    public class PowerSourceRemovingEventArgs : EventArgs
    {
        public PowerSourceRemovingEventArgs(string uniqueId, IPowerInterface powerSource)
        {
            this.UniqueId    = uniqueId;
            this.PowerSource = powerSource;
        }

        public string UniqueId { get; set; }

        public IPowerInterface PowerSource { get; set; }
    }
}
