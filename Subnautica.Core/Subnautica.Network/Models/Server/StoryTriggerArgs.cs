namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class StoryTriggerArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.StoryTrigger;

        [Key(5)]
        public string GoalKey { get; set; }

        [Key(6)]
        public global::Story.GoalType GoalType { get; set; }

        [Key(7)]
        public StoryCinematicType CinematicType { get; set; }

        [Key(8)]
        public bool IsStoryGoalMuted { get; set; }

        [Key(9)]
        public bool IsTrigger { get; set; }

        [Key(10)]
        public bool IsPlayMuted { get; set; }

        [Key(11)]
        public bool IsClearSound { get; set; }

        [Key(12)]
        public float TriggerTime { get; set; }

        [Key(13)]
        public byte PlayerCount { get; set; }

        [Key(14)]
        public byte MaxPlayerCount { get; set; }
    }
}
