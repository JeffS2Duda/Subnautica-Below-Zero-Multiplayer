namespace Subnautica.Events.EventArgs
{
    using System;

    public class CrafterBeginEventArgs : EventArgs
    {
        public CrafterBeginEventArgs(string uniqueId, TechType fabricatorType, TechType techType, float duration, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.FabricatorType = fabricatorType;
            this.TechType = techType;
            this.Duration = duration;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public TechType FabricatorType { get; private set; }

        public TechType TechType { get; private set; }

        public float Duration { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
