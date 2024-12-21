namespace Subnautica.Events.EventArgs
{
    using System;

    public class GlowWhaleRideStartingEventArgs : EventArgs
    {
        public GlowWhaleRideStartingEventArgs(string uniqueId, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public bool IsAllowed { get; set; }
    }
}
