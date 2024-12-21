namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class StoryCallArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.StoryCall;

        [Key(5)]
        public string GoalKey { get; set; }

        [Key(6)]
        public string TargetGoalKey { get; set; }

        [Key(7)]
        public bool IsAnswered { get; set; }
    }
}
