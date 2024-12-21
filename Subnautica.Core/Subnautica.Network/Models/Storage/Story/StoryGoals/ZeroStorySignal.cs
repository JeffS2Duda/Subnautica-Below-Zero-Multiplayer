namespace Subnautica.Network.Models.Storage.Story.StoryGoals
{
    using global::Story;

    using MessagePack;

    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class ZeroStorySignal
    {
        [Key(0)]
        public string UniqueId { get; set; }

        [Key(1)]
        public UnlockSignalData.SignalType SignalType { get; set; }

        [Key(2)]
        public ZeroVector3 TargetPosition { get; set; }

        [Key(3)]
        public string TargetDescription { get; set; }

        [Key(4)]
        public bool IsVisited { get; set; }

        [Key(5)]
        public bool IsRemoved { get; set; }
    }
}
