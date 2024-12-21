namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using System;

    [MessagePackObject]
    public class SeaTruckTeleportationModule : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public LiveMixin LiveMixin { get; set; } = new LiveMixin(500f, 500f);

        public SeaTruckTeleportationModule Initialize(Action<NetworkDynamicEntityComponent> onEntityComponentInitialized)
        {
            onEntityComponentInitialized?.Invoke(this);
            return this;
        }
    }
}

