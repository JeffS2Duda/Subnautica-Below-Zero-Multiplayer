namespace Subnautica.Events.EventArgs
{
    using System;

    public class StorageItemRemovingEventArgs : EventArgs
    {
        public StorageItemRemovingEventArgs(string uniqueId, TechType techType, string itemId, Pickupable item, bool IsAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.TechType = techType;
            this.ItemId = itemId;
            this.Item = item;
            this.IsAllowed = IsAllowed;
        }

        public string UniqueId { get; set; }

        public TechType TechType { get; set; }

        public string ItemId { get; set; }

        public Pickupable Item { get; set; }

        public bool IsAllowed { get; set; }
    }
}
