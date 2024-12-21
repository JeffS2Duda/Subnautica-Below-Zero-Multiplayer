namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class VehicleExitArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.VehicleExit;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public TechType TechType { get; set; }

        [Key(7)]
        public ZeroVector3 Position { get; set; }

        [Key(8)]
        public ZeroQuaternion Rotation { get; set; }
    }
}
