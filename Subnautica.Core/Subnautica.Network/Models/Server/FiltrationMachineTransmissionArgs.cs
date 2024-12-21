namespace Subnautica.Network.Models.Server
{
    using LiteNetLib;
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Metadata;
    using System.Collections.Generic;

    [MessagePackObject]
    public class FiltrationMachineTransmissionArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.FiltrationMachineTransmission;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.EnergyTransmission;

        [Key(2)]
        public override DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.ReliableSequenced;

        [Key(3)]
        public override byte ChannelId { get; set; } = 1;

        [Key(5)]
        public List<FiltrationMachineTimeItem> TimeItems { get; set; }
    }
}
