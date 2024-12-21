namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class StoryShieldBaseArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.StoryShieldBase;

        [Key(5)]
        public bool IsEntered { get; set; }

        [Key(6)]
        public float Time { get; set; }
    }
}
