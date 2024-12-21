namespace Subnautica.Events.EventArgs
{
    using Subnautica.API.Enums;
    using System;

    public class CinematicTriggeringEventArgs : EventArgs
    {
        public CinematicTriggeringEventArgs(string uniqueId, StoryCinematicType cinematicType, bool isClicked = false, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.StoryCinematicType = cinematicType;
            this.IsClicked = isClicked;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public StoryCinematicType StoryCinematicType { get; private set; }

        public bool IsClicked { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
