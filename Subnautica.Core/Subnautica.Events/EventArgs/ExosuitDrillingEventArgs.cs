namespace Subnautica.Events.EventArgs
{
    using System;
    using System.Collections.Generic;

    using Subnautica.API.Features;
    using Subnautica.Network.Structures;

    public class ExosuitDrillingEventArgs : EventArgs
    {
        public ExosuitDrillingEventArgs(string uniqueId, string slotId, float maxHealth, TechType dropTechType, List<ZeroVector3> dropPositions, bool isMultipleDrill, bool isAllowed = true)
        {
            this.UniqueId            = uniqueId;
            this.SlotId              = slotId;
            this.MaxHealth           = maxHealth;
            this.DropTechType        = dropTechType;
            this.DropPositions       = dropPositions;
            this.IsMultipleDrill     = isMultipleDrill;
            this.IsAllowed           = isAllowed;
            this.IsStaticWorldEntity = Network.StaticEntity.IsStaticEntity(slotId);
        }

        public string UniqueId { get; set; }

        public string SlotId { get; set; }

        public float MaxHealth { get; set; }

        public TechType DropTechType { get; set; }

        public List<ZeroVector3> DropPositions { get; set; }

        public bool IsMultipleDrill { get; set; }

        public bool IsAllowed { get; set; }

        public bool IsStaticWorldEntity { get; set; }
    }
}
