namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Sign : MetadataComponent
    {
        [Key(0)]
        public string Text { get; set; }

        [Key(1)]
        public bool[] ElementsState { get; set; }

        [Key(2)]
        public int ScaleIndex { get; set; }

        [Key(3)]
        public int ColorIndex { get; set; }

        [Key(4)]
        public bool IsBackgroundEnabled { get; set; }

        [Key(5)]
        public bool IsOpening { get; set; }

        [Key(6)]
        public bool IsSave { get; set; }
    }
}
