namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using System;

    [MessagePackObject]
    public class SeaTruckDockingModule : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public LiveMixin LiveMixin { get; set; } = new LiveMixin(500f, 500f);

        [Key(1)]
        public WorldDynamicEntity Vehicle { get; set; }

        public bool Dock(WorldDynamicEntity entity)
        {
            if (this.IsDocked())
            {
                return false;
            }

            this.Vehicle = entity;
            return true;
        }

        public bool Undock(out WorldDynamicEntity vehicle)
        {
            vehicle = this.Vehicle;

            if (this.IsDocked())
            {
                this.Vehicle = null;
                return true;
            }

            return false;
        }

        public bool IsDocked()
        {
            return this.Vehicle != null;
        }

        public SeaTruckDockingModule Initialize(Action<NetworkDynamicEntityComponent> onEntityComponentInitialized)
        {
            onEntityComponentInitialized?.Invoke(this);
            return this;
        }
    }
}
