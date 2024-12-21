namespace Subnautica.Network.Models.Server
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;

    using EntityModel = Subnautica.Network.Models.WorldEntity;
    
    [MessagePackObject]
    public class ExosuitDrillArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.ExosuitDrill;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public string SlotId { get; set; }

        [Key(7)]
        public float MaxHealth { get; set; }

        [Key(8)]
        public float NewHealth { get; set; }

        [Key(9)]
        public TechType DropTechType { get; set; }

        [Key(10)]
        public List<ZeroVector3> DropPositions { get; set; }

        [Key(11)]
        public List<WorldPickupItem> InventoryItems { get; set; } = new List<WorldPickupItem>();

        [Key(12)]
        public List<WorldDynamicEntity> WorldItems { get; set; } = new List<WorldDynamicEntity>();

        [Key(13)]
        public WorldPickupItem DisableItem { get; set; }

        [Key(14)]
        public bool IsMultipleDrill { get; set; }

        [Key(15)]
        public bool IsStaticWorldEntity { get; set; }

        [Key(16)]
        public EntityModel.Drillable StaticEntity { get; set; }
    }
}
