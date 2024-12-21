namespace Subnautica.Network.Models.Server
{
    using LiteNetLib;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class PlayerStatsArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.PlayerStats;

        [Key(2)]
        public override DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.Unreliable;

        [Key(5)]
        public float Health { get; set; }

        [Key(6)]
        public float Water { get; set; }

        [Key(7)]
        public float Food { get; set; }
    }
}
