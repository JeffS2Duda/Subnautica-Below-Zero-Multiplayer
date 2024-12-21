namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class ConstructionHealthArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.ConstructionHealth;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.Construction;

        [Key(3)]
        public override byte ChannelId { get; set; } = 1;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public float Damage { get; set; }

        [Key(7)]
        public float MaxHealth { get; set; }
    }
}
