namespace Subnautica.Events.EventArgs
{
    using System;

    public class InventoryItemAddedEventArgs : EventArgs
    {
        public InventoryItemAddedEventArgs(string uniqueId, Pickupable item)
        {
            this.UniqueId = uniqueId;
            this.Item = item;
        }

        public string UniqueId { get; set; }

        public Pickupable Item { get; set; }
    }
}
