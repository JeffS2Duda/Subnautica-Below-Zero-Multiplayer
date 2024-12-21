namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class CosmeticItemArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.CosmeticItem;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public string BaseId { get; set; }

        [Key(7)]
        public TechType TechType { get; set; }

        [Key(8)]
        public ZeroVector3 Position { get; set; }

        [Key(9)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(10)]
        public WorldPickupItem PickupItem { get; set; }

        [Key(11)]
        public CosmeticItem CosmeticItem { get; set; }
    }
}
