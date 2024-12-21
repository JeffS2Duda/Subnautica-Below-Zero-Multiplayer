namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class StoryInteractArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.StoryInteract;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public string GoalKey { get; set; }

        [Key(7)]
        public StoryCinematicType CinematicType { get; set; }
    }
}
