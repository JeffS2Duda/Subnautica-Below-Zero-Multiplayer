namespace Subnautica.Events.EventArgs
{
    using System;

    public class StoryCallingEventArgs : EventArgs
    {
        public StoryCallingEventArgs(string callGoalKey, string targetGoalKey, bool isAnswered, bool isAllowed = true)
        {
            this.CallGoalKey   = callGoalKey;
            this.TargetGoalKey = targetGoalKey;
            this.IsAnswered    = isAnswered;
            this.IsAllowed     = isAllowed;
        }

        public string CallGoalKey { get; private set; }

        public string TargetGoalKey { get; private set; }

        public bool IsAnswered { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
