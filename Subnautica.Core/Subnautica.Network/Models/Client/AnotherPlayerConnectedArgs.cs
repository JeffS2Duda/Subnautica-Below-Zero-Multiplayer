namespace Subnautica.Network.Models.Client
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class AnotherPlayerConnectedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.AnotherPlayerConnected;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public byte PlayerId { get; set; }

        [Key(7)]
        public string SubrootId { get; set; }

        [Key(8)]
        public string InteriorId { get; set; }

        [Key(9)]
        public string PlayerName { get; set; }

        [Key(10)]
        public ZeroVector3 Position { get; set; }

        [Key(11)]
        public ZeroQuaternion Rotation { get; set; }
    }
}
