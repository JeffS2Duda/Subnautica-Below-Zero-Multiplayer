namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class PlayerDisconnectedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.PlayerDisconnected;

        [Key(5)]
        public string UniqueId { get; set; }
    }
}
