namespace Subnautica.Network.Models.Items
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class SpyPenguin : NetworkPlayerItemComponent
    {
        [Key(1)]
        public override TechType TechType { get; set; } = TechType.SpyPenguin;

        [Key(2)]
        public ZeroVector3 Position { get; set; }

        [Key(3)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(4)]
        public string Name { get; set; }

        [Key(5)]
        public float Health { get; set; }

        [Key(6)]
        public List<byte[]> Items { get; set; }

        [Key(7)]
        public float SpawnChance { get; set; } = -1f;

        [Key(8)]
        public WorldPickupItem WorldPickupItem { get; set; }

        [Key(9)]
        public WorldDynamicEntity Entity { get; set; }

        [Key(10)]
        public bool IsPickup { get; set; }

        [Key(11)]
        public bool IsStalkerFur { get; set; }

        [Key(12)]
        public bool IsDeploy { get; set; }

        [Key(13)]
        public bool IsAdded { get; set; }
    }
}
