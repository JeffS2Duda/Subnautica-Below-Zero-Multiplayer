namespace Subnautica.Network.Models.Server
{
    using System.Collections.Generic;

    using LiteNetLib;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class HoverpadChargeTransmissionArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.HoverpadChargeTransmission;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.EnergyTransmission;

        [Key(2)]
        public override DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.ReliableSequenced;

        [Key(5)]
        public Dictionary<uint, HoverpadEnergyTransmissionItem> Items { get; set; }
    }

    [MessagePackObject]
    public class HoverpadEnergyTransmissionItem
    {
        [Key(0)]
        public byte Health { get; set; }

        [Key(1)]
        public byte Charge { get; set; }

        public HoverpadEnergyTransmissionItem()
        {

        }

        public HoverpadEnergyTransmissionItem(byte health, byte charge)
        {
            this.Health = health;
            this.Charge = charge;
        }
    }
}
