namespace Subnautica.Events.EventArgs
{
    using System;

    public class SpyPenguinItemGrabingEventArgs : EventArgs
    {
        public SpyPenguinItemGrabingEventArgs(string uniqueId, string animationName)
        {
            this.UniqueId = uniqueId;
            this.AnimationName = animationName;
        }

        public string UniqueId { get; set; }

        public string AnimationName { get; set; }
    }
}
