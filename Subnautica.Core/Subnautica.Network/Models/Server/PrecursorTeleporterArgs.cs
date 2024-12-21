namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class PrecursorTeleporterArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.PrecursorTeleporter;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public string TeleporterId { get; set; }

        [Key(7)]
        public bool IsTerminal { get; set; }

        [Key(8)]
        public bool IsTeleportStart { get; set; }

        [Key(9)]
        public bool IsTeleportCompleted { get; set; }
    }
}
