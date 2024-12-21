namespace Subnautica.Events.EventArgs
{
    using System;

    public class LilyPaddlerHypnotizeStartingEventArgs : EventArgs
    {
        public LilyPaddlerHypnotizeStartingEventArgs(string creatureId, byte targetId, bool isAllowed = true)
        {
            this.CreatureId = creatureId;
            this.TargetId = targetId;
            this.IsAllowed = isAllowed;
        }

        public string CreatureId { get; set; }

        public byte TargetId { get; set; }

        public bool IsAllowed { get; set; }
    }
}
