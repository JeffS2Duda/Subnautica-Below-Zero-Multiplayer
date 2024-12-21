namespace Subnautica.Events.EventArgs
{
    using System;

    public class HoverpadShowroomTriggeringEventArgs : EventArgs
    {
        public HoverpadShowroomTriggeringEventArgs(string uniqueId, bool isEnter, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.IsEnter = isEnter;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public bool IsEnter { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
