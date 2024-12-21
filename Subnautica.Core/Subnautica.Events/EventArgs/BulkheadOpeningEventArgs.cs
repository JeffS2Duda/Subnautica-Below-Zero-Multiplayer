namespace Subnautica.Events.EventArgs
{
    using System;

    using Subnautica.API.Enums;
    using Subnautica.API.Features;

    public class BulkheadOpeningEventArgs : EventArgs
    {
        public BulkheadOpeningEventArgs(string uniqueId, bool side, StoryCinematicType storyCinematicType, bool isAllowed = true)
        {
            this.UniqueId            = uniqueId;
            this.Side                = side;
            this.IsAllowed           = isAllowed;
            this.StoryCinematicType  = storyCinematicType;
            this.IsStaticWorldEntity = Network.StaticEntity.IsStaticEntity(uniqueId);
        }

        public string UniqueId { get; set; }

        public bool Side { get; set; }

        public bool IsStaticWorldEntity { get; set; }

        public StoryCinematicType StoryCinematicType { get; set; }

        public bool IsAllowed { get; set; }
    }
}
