namespace Subnautica.Events.EventArgs
{
    using System;

    public class CrafterEndedEventArgs : EventArgs
    {
        public CrafterEndedEventArgs(string uniqueId, TechType crafterTechType, TechType techType, global::GhostCrafter crafter)
        {
            this.UniqueId        = uniqueId;
            this.CrafterTechType = crafterTechType;
            this.TechType        = techType;
            this.Crafter         = crafter;
        }

        public string UniqueId { get; set; }

        public TechType CrafterTechType { get; set; }

        public TechType TechType { get; set; }

        public global::GhostCrafter Crafter { get; set; }
    }
}
