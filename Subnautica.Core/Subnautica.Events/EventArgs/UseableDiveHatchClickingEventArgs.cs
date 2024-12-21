namespace Subnautica.Events.EventArgs
{
    using System;

    public class UseableDiveHatchClickingEventArgs : EventArgs
    {
        public UseableDiveHatchClickingEventArgs(string uniqueId, bool isEnter, string playerViewAnimation, bool isMoonpoolExpansion, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.IsEnter = isEnter;
            this.IsBulkHead = playerViewAnimation.Contains("surfacebasedoor_");
            this.IsLifePod = playerViewAnimation.Contains("droppod_");
            this.IsMoonpoolExpansion = isMoonpoolExpansion;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public bool IsEnter { get; set; }

        public bool IsBulkHead { get; set; }

        public bool IsLifePod { get; set; }

        public bool IsMoonpoolExpansion { get; set; }

        public bool IsAllowed { get; set; }
    }
}
