namespace Subnautica.Network.Models.Server
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class ConstructionAmountChangedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.ConstructingAmountChanged;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.Construction;

        [Key(3)]
        public override byte ChannelId { get; set; } = 1;

        [Key(5)]
        public TechType TechType { get; set; }

        [Key(6)]
        public string UniqueId { get; set; }

        [Key(7)]
        public bool IsConstruct { get; set; }

        [Key(8)]
        public float Amount { get; set; }
    }
}
