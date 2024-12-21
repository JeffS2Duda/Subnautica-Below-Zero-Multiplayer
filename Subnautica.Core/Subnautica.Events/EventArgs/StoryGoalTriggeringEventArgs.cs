namespace Subnautica.Events.EventArgs
{
    using System;

    using Subnautica.API.Enums;

    public class StoryGoalTriggeringEventArgs : EventArgs
    {
        public StoryGoalTriggeringEventArgs(string storyKey, global::Story.GoalType goalType, bool isPlayMuted, bool isStoryGoalMuted = false, StoryCinematicType cinematicType = StoryCinematicType.None, bool isAllowed = true)
        {
            this.StoryKey         = storyKey;
            this.GoalType         = goalType;
            this.IsPlayMuted      = isPlayMuted;
            this.IsStoryGoalMuted = isStoryGoalMuted;
            this.CinematicType    = cinematicType;
            this.IsAllowed        = isAllowed;
        }

        public string StoryKey { get; set; }

        public global::Story.GoalType GoalType { get; set; }

        public bool IsPlayMuted { get; set; }

        public bool IsStoryGoalMuted { get; set; }

        public StoryCinematicType CinematicType { get; set; }

        public bool IsAllowed { get; set; }
    }
}
