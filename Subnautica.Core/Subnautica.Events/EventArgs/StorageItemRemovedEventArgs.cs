namespace Subnautica.Events.EventArgs
{
    using System;

    public class StorageItemRemovedEventArgs : EventArgs
    {
        public StorageItemRemovedEventArgs(string constructionId, TechType techType, string itemId, Pickupable item)
        {
            this.ConstructionId = constructionId;
            this.TechType       = techType;
            this.ItemId         = itemId;
            this.Item           = item;
        }

        public string ConstructionId { get; set; }

        public TechType TechType { get; set; }

        public string ItemId { get; set; }

        public Pickupable Item { get; set; }
    }
}
