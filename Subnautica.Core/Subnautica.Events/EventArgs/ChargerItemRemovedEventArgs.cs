namespace Subnautica.Events.EventArgs
{
    using System;

    public class ChargerItemRemovedEventArgs : EventArgs
    {
        public ChargerItemRemovedEventArgs(string constructionId, string slotId, TechType techType, string itemId, Pickupable item)
        {
            this.ConstructionId = constructionId;
            this.SlotId         = slotId;
            this.TechType       = techType;
            this.ItemId         = itemId;
            this.Item           = item;
        }

        public string ConstructionId { get; set; }

        public string SlotId { get; set; }

        public TechType TechType { get; set; }

        public string ItemId { get; set; }

        public Pickupable Item { get; set; }
    }
}
