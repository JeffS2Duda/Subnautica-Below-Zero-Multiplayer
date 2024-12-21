namespace Subnautica.Events.EventArgs
{
    using Subnautica.API.Features;
    using System;

    public class BulkheadClosingEventArgs : EventArgs
    {
        public BulkheadClosingEventArgs(string uniqueId, bool side, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Side = side;
            this.IsAllowed = isAllowed;
            this.IsStaticWorldEntity = Network.StaticEntity.IsStaticEntity(uniqueId);
        }

        public string UniqueId { get; set; }

        public bool Side { get; set; }

        public bool IsStaticWorldEntity { get; set; }

        public bool IsAllowed { get; set; }
    }
}
