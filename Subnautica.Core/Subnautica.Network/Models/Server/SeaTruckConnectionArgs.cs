namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class SeaTruckConnectionArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.SeaTruckConnection;

        [Key(5)]
        public bool IsConnect { get; set; }

        [Key(6)]
        public bool IsEject { get; set; }

        [Key(7)]
        public bool IsMoonpoolExpansion { get; set; }

        [Key(8)]
        public string FrontModuleId { get; set; }

        [Key(9)]
        public string BackModuleId { get; set; }

        [Key(10)]
        public string FirstModuleId { get; set; }

        [Key(11)]
        public ushort ModuleId { get; set; }

        [Key(12)]
        public ZeroVector3 Position { get; set; }

        [Key(13)]
        public ZeroQuaternion Rotation { get; set; }
    }
}
