namespace Subnautica.Events.EventArgs
{
    using System;

    public class FurnitureDeconstructionBeginEventArgs : EventArgs
    {
        public FurnitureDeconstructionBeginEventArgs(string uniqueId, TechType techType, bool isAllowed = true)
        {
            this.UniqueId  = uniqueId;
            this.TechType  = techType;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public TechType TechType { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
