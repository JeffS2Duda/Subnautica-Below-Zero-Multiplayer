namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class UseableDiveHatchArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.UseableDiveHatch;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public bool IsBulkHead { get; set; }

        [Key(7)]
        public bool IsLifePod { get; set; }

        [Key(8)]
        public bool IsEnter { get; set; }

        [Key(9)]
        public bool IsMoonpoolExpansion { get; set; }
    }
}
