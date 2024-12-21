namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;

    using WorldEntityModel = Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;

    [MessagePackObject]
    public class Hoverbike : NetworkPlayerItemComponent
    {
        [Key(1)]
        public override TechType TechType { get; set; } = TechType.Hoverbike;

        [Key(4)]
        public ZeroVector3 Position { get; set; }

        [Key(5)]
        public ZeroVector3 Forward { get; set; }

        [Key(6)]
        public WorldEntityModel.Hoverbike Component { get; set; }

        [Key(7)]
        public WorldDynamicEntity Entity { get; set; }
    }
}
