namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class StoryCinematicTriggerArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.StoryCinematicTrigger;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public StoryCinematicType CinematicType { get; set; }

        [Key(7)]
        public double StartTime { get; set; }

        [Key(8)]
        public bool IsTypeClick { get; set; }
    }
}
