namespace Subnautica.Events.EventArgs
{
    using System;

    public class PowerSourceAddingEventArgs : EventArgs
    {
        public PowerSourceAddingEventArgs(string uniqueId, IPowerInterface powerSource)
        {
            this.UniqueId = uniqueId;
            this.PowerSource = powerSource;
        }

        public string UniqueId { get; set; }

        public IPowerInterface PowerSource { get; set; }
    }
}
