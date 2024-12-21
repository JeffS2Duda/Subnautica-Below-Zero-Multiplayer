namespace Subnautica.Events.EventArgs
{
    using System;

    public class TeleportationToolUsedEventArgs : EventArgs
    {
        public TeleportationToolUsedEventArgs(string teleporterId)
        {
            this.TeleporterId = teleporterId;
        }

        public string TeleporterId { get; set; }
    }
}
