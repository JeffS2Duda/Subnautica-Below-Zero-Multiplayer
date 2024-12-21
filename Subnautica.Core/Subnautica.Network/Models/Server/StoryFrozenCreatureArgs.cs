namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class StoryFrozenCreatureArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.StoryFrozenCreature;

        [Key(5)]
        public StoryCinematicType CinematicType { get; set; }

        [Key(6)]
        public float InjectTime { get; set; }

        [Key(7)]
        public bool IsDenied { get; set; }
    }
}
