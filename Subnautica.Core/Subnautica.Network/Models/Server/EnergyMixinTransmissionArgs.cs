namespace Subnautica.Network.Models.Server
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;
    using System.Collections.Generic;

    [MessagePackObject]
    public class EnergyMixinTransmissionArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.EnergyMixinTransmission;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.EnergyTransmission;

        [Key(5)]
        public List<EnergyMixinTransmissionItem> Items { get; set; }
    }

    [MessagePackObject]
    public class EnergyMixinTransmissionItem
    {
        [Key(0)]
        public ushort ItemId { get; set; }

        [Key(1)]
        public float Charge { get; set; }

        [IgnoreMember]
        public ZeroVector3 Position { get; set; }

        public EnergyMixinTransmissionItem()
        {

        }

        public EnergyMixinTransmissionItem(ushort itemId, float charge, ZeroVector3 position)
        {
            this.ItemId = itemId;
            this.Charge = charge;
            this.Position = position;
        }
    }
}
