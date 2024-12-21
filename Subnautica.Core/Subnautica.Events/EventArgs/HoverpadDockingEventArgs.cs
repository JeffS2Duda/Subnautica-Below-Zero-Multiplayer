namespace Subnautica.Events.EventArgs
{
    using System;

    public class HoverpadDockingEventArgs : EventArgs
    {
        public HoverpadDockingEventArgs(string uniqueId, string itemId, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.ItemId = itemId;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public string ItemId { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
