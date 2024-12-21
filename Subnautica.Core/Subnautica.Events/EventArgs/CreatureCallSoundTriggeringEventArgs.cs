namespace Subnautica.Events.EventArgs
{
    using System;

    public class CreatureCallSoundTriggeringEventArgs : EventArgs
    {
        public CreatureCallSoundTriggeringEventArgs(string uniqueId, byte callId, string animation = null, bool isAllowed = true)
        {
            this.UniqueId  = uniqueId;
            this.CallId    = callId;
            this.Animation = animation;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public byte CallId { get; set; }

        public string Animation { get; set; }

        public bool IsAllowed { get; set; }
    }
}
