namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class StoryBridgeArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.StoryBridge;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public string StoryKey { get; set; }

        [Key(7)]
        public bool IsClickedFluid { get; set; }

        [Key(8)]
        public bool IsClickedExtend { get; set; }

        [Key(9)]
        public bool IsClickedRetract { get; set; }

        [Key(10)]
        public bool IsFirstExtension { get; set; }

        [Key(11)]
        public float Time { get; set; }
    }
}
