namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class MetalDetector : NetworkPlayerItemComponent
    {
        [Key(2)]
        public TechType TechTypeIndex { get; set; }

        [Key(3)]
        public global::MetalDetector.ScreenState ScreenState { get; set; }

        [Key(4)]
        public bool IsUsing { get; set; }

        [Key(5)]
        public float Wiggle { get; set; }
    }
}
