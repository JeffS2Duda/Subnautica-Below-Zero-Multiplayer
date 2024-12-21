namespace Subnautica.Events.EventArgs
{
    using System;

    public class GlowWhaleRideStopedEventArgs : EventArgs
    {
        public GlowWhaleRideStopedEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; set; }
    }
}
