namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class PipeSurfaceFloater : NetworkPlayerItemComponent
    {
        [Key(1)]
        public override TechType TechType { get; set; } = TechType.PipeSurfaceFloater;

        [Key(2)]
        public byte ProcessType { get; set; }

        [Key(3)]
        public string PipeId { get; set; }

        [Key(4)]
        public string ParentId { get; set; }

        [Key(5)]
        public ZeroVector3 Position { get; set; }

        [Key(6)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(7)]
        public WorldDynamicEntity Entity { get; set; }

        [Key(8)]
        public WorldPickupItem PickupItem { get; set; }

        public bool IsSurfaceFloaterDeploy()
        {
            return this.ProcessType == 1;
        }

        public bool IsOxygenPipePlace()
        {
            return this.ProcessType == 3;
        }

        public bool IsOxygenPipePickup()
        {
            return this.ProcessType == 4;
        }
    }
}
