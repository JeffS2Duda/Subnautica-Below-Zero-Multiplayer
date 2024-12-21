namespace Subnautica.Events.EventArgs
{
    using System;

    public class StorageItemAddedEventArgs : EventArgs
    {
        public StorageItemAddedEventArgs(string constructionId, TechType techType, string itemId, Pickupable item, InventoryItem inventoryItem)
        {
            this.ConstructionId = constructionId;
            this.TechType       = techType;
            this.ItemId         = itemId;
            this.Item           = item;
            this.InventoryItem  = inventoryItem;
        }

        public string ConstructionId { get; set; }

        public TechType TechType { get; set; }

        public string ItemId { get; set; }

        public Pickupable Item { get; set; }

        public InventoryItem InventoryItem { get; set; }
    }
}
