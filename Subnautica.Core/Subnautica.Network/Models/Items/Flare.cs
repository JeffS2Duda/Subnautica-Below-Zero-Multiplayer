namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class Flare : NetworkPlayerItemComponent
    {
        [Key(1)]
        public override TechType TechType { get; set; } = TechType.Flare;

        [Key(2)]
        public ZeroVector3 Position { get; set; }

        [Key(3)]
        public ZeroVector3 Forward { get; set; }
        [Key(4)]
        public ZeroQuaternion Rotation { get; set; }
        [Key(5)]
        public float Intensity { get; set; }

        [Key(6)]
        public float Range { get; set; }

        [Key(7)]
        public float Energy { get; set; }

        [Key(8)]
        public WorldDynamicEntity Entity { get; set; }
    }
}
