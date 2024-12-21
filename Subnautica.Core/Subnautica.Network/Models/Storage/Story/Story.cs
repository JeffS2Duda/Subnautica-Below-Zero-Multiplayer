namespace Subnautica.Network.Models.Storage.Story
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Storage.Story.Components;
    using Subnautica.Network.Models.Storage.Story.StoryGoals;
    using System;
    using System.Collections.Generic;

    [MessagePackObject]
    [Serializable]
    public class Story
    {
        [Key(0)]
        public HashSet<ZeroStoryGoal> CompletedGoals { get; set; } = new HashSet<ZeroStoryGoal>();

        [Key(1)]
        public HashSet<ZeroStorySignal> Signals { get; set; } = new HashSet<ZeroStorySignal>();

        [Key(2)]
        public List<StoryCinematicType> CompletedCinematics { get; set; } = new List<StoryCinematicType>();

        [Key(3)]
        public List<string> CompletedCalls { get; set; } = new List<string>();

        [Key(4)]
        public string IncomingCallGoalKey { get; set; } = null;

        [Key(5)]
        public GlacialBasinBridgeComponent Bridge { get; set; } = new GlacialBasinBridgeComponent();

        [Key(6)]
        public HashSet<string> CompletedTriggers { get; set; } = new HashSet<string>();

        [Key(7)]
        public List<CustomDoorwayComponent> CustomDoorways { get; set; } = new List<CustomDoorwayComponent>();

        [Key(8)]
        public FrozenCreatureComponent FrozenCreature { get; set; } = new FrozenCreatureComponent();

        [Key(9)]
        public ShieldBaseComponent ShieldBase { get; set; } = new ShieldBaseComponent();
    }
}
