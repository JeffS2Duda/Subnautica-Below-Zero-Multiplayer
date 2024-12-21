namespace Subnautica.Network.Models.Storage.Story.StoryGoals
{
    using MessagePack;

    [MessagePackObject]
    public class ZeroStoryGoal
    {
        [Key(0)]
        public string Key { get; set; }

        [Key(1)]
        public global::Story.GoalType GoalType { get; set; }

        [Key(2)]
        public bool IsPlayMuted { get; set; }

        [Key(3)]
        public float FinishedTime { get; set; }
    }
}
