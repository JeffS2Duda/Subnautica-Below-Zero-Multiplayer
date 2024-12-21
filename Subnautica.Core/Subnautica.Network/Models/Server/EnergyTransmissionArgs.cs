namespace Subnautica.Network.Models.Server
{
    using LiteNetLib;
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using System.Collections.Generic;

    [MessagePackObject]
    public class EnergyTransmissionArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.EnergyTransmission;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.EnergyTransmission;

        [Key(2)]
        public override DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.ReliableSequenced;

        [Key(5)]
        public Dictionary<uint, float> PowerSources { get; set; }
    }
}
