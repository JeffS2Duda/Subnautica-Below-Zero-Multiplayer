namespace Subnautica.Events.EventArgs
{
    using System;

    public class NuclearReactorItemRemovedEventArgs : EventArgs
    {
        public NuclearReactorItemRemovedEventArgs(string constructionId, string slotId, string itemId, Pickupable item)
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
