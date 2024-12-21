namespace Subnautica.Events.EventArgs
{
    using System;

    public class CrafterItemPickupEventArgs : EventArgs
    {
        public CrafterItemPickupEventArgs(string uniqueId, global::GhostCrafter crafter, int amount, TechType fabricatorType, TechType techType, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Crafter = crafter;
            this.Amount = amount;
            this.FabricatorType = fabricatorType;
            this.TechType = techType;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public global::GhostCrafter Crafter { get; private set; }

        public int Amount { get; private set; }

        public TechType FabricatorType { get; private set; }

        public TechType TechType { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
