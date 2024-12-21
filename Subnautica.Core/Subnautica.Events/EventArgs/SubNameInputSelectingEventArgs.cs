namespace Subnautica.Events.EventArgs
{
    using System;

    public class SubNameInputSelectingEventArgs : EventArgs
    {
        public SubNameInputSelectingEventArgs(string uniqueId, TechType techType, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.TechType = techType;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public TechType TechType { get; set; }

        public bool IsAllowed { get; set; }
    }
}
