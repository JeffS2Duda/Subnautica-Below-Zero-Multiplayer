namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerClimbingEventArgs : EventArgs
    {
        public PlayerClimbingEventArgs(string uniqueId, float duration, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Duration = duration;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public float Duration { get; set; }

        public bool IsAllowed { get; set; }
    }
}
