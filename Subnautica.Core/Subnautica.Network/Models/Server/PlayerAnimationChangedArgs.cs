namespace Subnautica.Network.Models.Server
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using System.Collections.Generic;

    [MessagePackObject]
    public class PlayerAnimationChangedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.PlayerAnimationChanged;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.PlayerAnimation;

        [Key(5)]
        public Dictionary<PlayerAnimationType, bool> Animations { get; set; }
    }
}


