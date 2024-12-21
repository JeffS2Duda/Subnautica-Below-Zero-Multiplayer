namespace Subnautica.Network.Models.Server
{
    using System.Collections.Generic;

    using LiteNetLib;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class BaseMapRoomTransmissionArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.BaseMapRoomTransmission;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.EnergyTransmission;

        [Key(2)]
        public override DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.ReliableSequenced;

        [Key(3)]
        public override byte ChannelId { get; set; } = 1;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public List<BaseMapRoomTransmissionItem> Items { get; set; }
    }

    [MessagePackObject]
    public class BaseMapRoomTransmissionItem
    {
        [Key(0)]
        public string UniqueId { get; set; }

        [Key(1)]
        public long Position { get; set; }

        public BaseMapRoomTransmissionItem()
        {

        }

        public BaseMapRoomTransmissionItem(string uniqueId, long position)
        {
            this.UniqueId = uniqueId;
            this.Position = position;
        }
    }
}
