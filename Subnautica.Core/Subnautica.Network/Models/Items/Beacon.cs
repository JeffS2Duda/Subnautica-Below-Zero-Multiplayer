namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class Beacon : NetworkPlayerItemComponent
    {
        [Key(1)]
        public override TechType TechType { get; set; } = TechType.Beacon;

        [Key(2)]
        public ZeroVector3 Position { get; set; }

        [Key(3)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(4)]
        public bool IsDeployedOnLand { get; set; }

        [Key(5)]
        public string Text { get; set; }

        [Key(6)]
        public bool IsTextChanged { get; set; }

        [Key(7)]
        public WorldDynamicEntity Entity { get; set; }
    }
}
