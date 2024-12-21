namespace Subnautica.Network.Models.Client
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class ConnectionRejectArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.None;

        [Key(5)]
        public ConnectionSignal RejectType { get; set; } = ConnectionSignal.Rejected;
    }
}
