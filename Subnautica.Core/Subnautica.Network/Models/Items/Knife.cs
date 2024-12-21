namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class Knife : NetworkPlayerItemComponent
    {
        [Key(1)]
        public override TechType TechType { get; set; } = TechType.Knife;

        [Key(4)]
        public VFXEventTypes VFXEventType { get; set; }

        [Key(5)]
        public ZeroVector3 TargetPosition { get; set; }

        [Key(6)]
        public ZeroVector3 Orientation { get; set; }

        [Key(7)]
        public VFXSurfaceTypes SurfaceType { get; set; }

        [Key(8)]
        public VFXSurfaceTypes SoundSurfaceType { get; set; }

        [Key(9)]
        public bool IsUnderwater { get; set; }
    }
}
