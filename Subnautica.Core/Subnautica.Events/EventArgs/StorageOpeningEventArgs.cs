namespace Subnautica.Events.EventArgs
{
    using System;

    public class StorageOpeningEventArgs : EventArgs
    {
        public StorageOpeningEventArgs(string constructionId, TechType techType, bool isAllowed = true)
        {
            this.ConstructionId = constructionId;
            this.TechType = techType;
            this.IsAllowed = isAllowed;
        }

        public string ConstructionId { get; set; }

        public TechType TechType { get; set; }

        public bool IsAllowed { get; set; }
    }
}
