namespace Subnautica.Events.EventArgs
{
    using System;

    using Subnautica.API.Enums;

    public class StoryHandClickingEventArgs : EventArgs
    {
        public StoryHandClickingEventArgs(string uniqueId, string goalKey, StoryCinematicType cinematicType, bool isAllowed = true)
        {
            this.UniqueId      = uniqueId;
            this.GoalKey       = goalKey;
            this.CinematicType = cinematicType;
            this.IsAllowed     = isAllowed;
        }

        public string UniqueId { get; set; }

        public string GoalKey { get; set; }
        public StoryCinematicType CinematicType { get; set; }

        public bool IsAllowed { get; set; }
    }
}
