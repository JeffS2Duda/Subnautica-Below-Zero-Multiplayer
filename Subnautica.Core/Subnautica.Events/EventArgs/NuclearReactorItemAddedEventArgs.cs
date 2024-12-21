namespace Subnautica.Events.EventArgs
{
    using System;

    public class NuclearReactorItemAddedEventArgs : EventArgs
    {
        public NuclearReactorItemAddedEventArgs(string constructionId, string slotId, string itemId, Pickupable item)
        {
            this.ConstructionId = constructionId;
            this.SlotId = slotId;
            this.ItemId = itemId;
            this.Item = item;
        }

        public string ConstructionId { get; set; }

        public string SlotId { get; set; }

        public string ItemId { get; set; }

        public Pickupable Item { get; set; }
    }
}
