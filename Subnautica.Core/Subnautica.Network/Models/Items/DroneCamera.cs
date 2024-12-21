namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;

    using WorldEntityModel = Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;

    [MessagePackObject]
    public class DroneCamera : NetworkPlayerItemComponent
    {
        [Key(1)]
        public override TechType TechType { get; set; } = TechType.MapRoomCamera;

        [Key(2)]
        public ZeroVector3 Position { get; set; }

        [Key(3)]
        public ZeroVector3 Forward { get; set; }

        [Key(4)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(5)]
        public WorldDynamicEntity Entity { get; set; }

        [Key(6)]
        public WorldPickupItem PickupItem { get; set; }

        [Key(7)]
        public WorldEntityModel.MapRoomCamera Component { get; set; }
    }
}
