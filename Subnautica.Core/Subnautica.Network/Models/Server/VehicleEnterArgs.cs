namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class VehicleEnterArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.VehicleEnter;

        [Key(5)]
        public string CustomId { get; set; }

        [Key(6)]
        public string UniqueId { get; set; }

        [Key(7)]
        public TechType TechType { get; set; }

        [Key(8)]
        public ZeroVector3 Position { get; set; }

        [Key(9)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(10)]
        public float Health { get; set; }

        [Key(11)]
        public WorldDynamicEntity Vehicle { get; set; }
    }
}
