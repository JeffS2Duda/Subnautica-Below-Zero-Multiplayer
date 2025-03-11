namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlanterItemAddedEventArgs : EventArgs
    {
        public PlanterItemAddedEventArgs(string uniqueId, string itemId, Plantable plantable, int slotId, bool isLeft)
        {
            this.UniqueId = uniqueId;
            this.ItemId = itemId;
            this.Plantable = plantable;
            this.SlotId = slotId;
            this.IsLeft = isLeft;
        }

        public string UniqueId { get; set; }

        public string ItemId { get; set; }

        public Plantable Plantable { get; set; }

        public int SlotId { get; set; }

        public bool IsLeft { get; set; }
    }
}
