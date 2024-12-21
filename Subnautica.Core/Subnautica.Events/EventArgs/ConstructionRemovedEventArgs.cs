namespace Subnautica.Events.EventArgs
{
    using System;

    public class ConstructionRemovedEventArgs : EventArgs
    {
        public ConstructionRemovedEventArgs(TechType techType, string uniqueId, Int3? cell = null)
        {
            this.TechType = techType;
            this.UniqueId = uniqueId;
            this.Cell = cell;
        }

        public TechType TechType { get; private set; }

        public string UniqueId { get; private set; }

        public Int3? Cell { get; private set; }
    }
}
