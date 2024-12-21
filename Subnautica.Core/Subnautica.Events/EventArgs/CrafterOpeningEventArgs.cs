namespace Subnautica.Events.EventArgs
{
    using System;

    public class CrafterOpeningEventArgs : EventArgs
    {
        public CrafterOpeningEventArgs(string uniqueId, TechType fabricatorType, bool isAllowed = true)
        {
            this.UniqueId       = uniqueId;
            this.FabricatorType = fabricatorType;
            this.IsAllowed      = isAllowed;
        }

        public string UniqueId { get; private set; }

        public TechType FabricatorType { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
