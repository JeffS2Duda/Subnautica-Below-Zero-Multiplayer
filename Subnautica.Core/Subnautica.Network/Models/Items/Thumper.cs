namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class Thumper : NetworkPlayerItemComponent
    {
        [Key(1)]
        public override TechType TechType { get; set; } = TechType.Thumper;

        [Key(2)]
        public ZeroVector3 Position { get; set; }

        [Key(3)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(4)]
        public float Charge { get; set; }

        [Key(5)]
        public WorldDynamicEntity Entity { get; set; }
    }
}
