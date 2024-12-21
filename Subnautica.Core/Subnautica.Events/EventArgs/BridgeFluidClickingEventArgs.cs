namespace Subnautica.Events.EventArgs
{
    using System;

    public class BridgeFluidClickingEventArgs : EventArgs
    {
        public BridgeFluidClickingEventArgs(string uniqueId, string storyKey, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.StoryKey = storyKey;
        }

        public string UniqueId { get; private set; }

        public string StoryKey { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
