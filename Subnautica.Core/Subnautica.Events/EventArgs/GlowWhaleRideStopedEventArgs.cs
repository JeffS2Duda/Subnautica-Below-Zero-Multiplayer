namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class GlowWhaleRideStopedEventArgs : EventArgs
    {
        public GlowWhaleRideStopedEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; set; }
    }
}
