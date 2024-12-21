namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlanterItemAddedEventArgs : EventArgs
    {
        public PlanterItemAddedEventArgs(string uniqueId, string itemId, Plantable plantable, int slotId)
        {
            this.UniqueId  = uniqueId;
            this.ItemId    = itemId;
            this.Plantable = plantable;
            this.SlotId    = slotId;
        }

        public string UniqueId { get; set; }

        public string ItemId { get; set; }

        public Plantable Plantable { get; set; }

        public int SlotId { get; set; }
    }
}
