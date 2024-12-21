namespace Subnautica.Events.EventArgs
{
    using System;

    public class SnowmanDestroyingEventArgs : EventArgs
    {
        public SnowmanDestroyingEventArgs(string uniqueId, bool isStaticWorldEntity, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.IsStaticWorldEntity = isStaticWorldEntity;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public bool IsStaticWorldEntity { get; set; }

        public bool IsAllowed { get; set; }
    }
}
