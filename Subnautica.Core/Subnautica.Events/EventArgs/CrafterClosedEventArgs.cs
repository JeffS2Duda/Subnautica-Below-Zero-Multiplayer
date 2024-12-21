namespace Subnautica.Events.EventArgs
{
    using System;

    public class CrafterClosedEventArgs : EventArgs
    {
        public CrafterClosedEventArgs(string uniqueId, TechType fabricatorType)
        {
            this.UniqueId = uniqueId;
            this.FabricatorType = fabricatorType;
        }

        public string UniqueId { get; private set; }

        public TechType FabricatorType { get; private set; }
    }
}
