namespace Subnautica.Events.EventArgs
{
    using System;

    using Subnautica.API.Features;

    public class SpyPenguinItemPickedUpEventArgs : EventArgs
    {
        public SpyPenguinItemPickedUpEventArgs(string uniqueId, string itemId, Pickupable item, bool isAllowed = true)
        {
            this.UniqueId  = uniqueId;
            this.ItemId    = itemId;
            this.Item      = item;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public string ItemId { get; private set; }

        public Pickupable Item { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
