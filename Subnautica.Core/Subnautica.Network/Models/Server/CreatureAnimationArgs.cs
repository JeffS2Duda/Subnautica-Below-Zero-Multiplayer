namespace Subnautica.Network.Models.Server
{
    using LiteNetLib;
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    using System.Collections.Generic;

    [MessagePackObject]
    public class CreatureAnimationArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.CreatureAnimation;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.FishMovement;

        [Key(2)]
        public override DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.Unreliable;

        [Key(5)]
        public HashSet<CreatureAnimationItem> Animations { get; set; } = new HashSet<CreatureAnimationItem>();
    }

    [MessagePackObject]
    public class CreatureAnimationItem
    {
        [Key(0)]
        public ushort CreatureId { get; set; }

        [Key(1)]
        public Dictionary<byte, byte> Animations = new Dictionary<byte, byte>();
    }
}
