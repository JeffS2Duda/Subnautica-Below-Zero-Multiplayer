namespace Subnautica.Events.EventArgs
{
    using System;

    public class DeconstructionBeginEventArgs : EventArgs
    {
        public DeconstructionBeginEventArgs(string uniqueId, global::BaseDeconstructable baseDeconstructable, TechType techType, bool isAllowed = true)
        {
            this.UniqueId            = uniqueId;
            this.BaseDeconstructable = baseDeconstructable;
            this.TechType            = techType;
            this.IsAllowed           = isAllowed;
        }

        public string UniqueId { get; private set; }

        public global::BaseDeconstructable BaseDeconstructable { get; private set; }

        public TechType TechType { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
