namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class JoiningServerArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.JoiningServer;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.Startup;

        [Key(5)]
        public string UserId { get; set; }

        [Key(6)]
        public string UserName { get; set; }

        [Key(7)]
        public bool IsReconnect { get; set; } = false;
    }
}
