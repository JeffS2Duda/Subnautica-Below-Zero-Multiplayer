namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Metadata;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using System;

    [MessagePackObject]
    public class SeaTruckSleeperModule : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public LiveMixin LiveMixin { get; set; } = new LiveMixin(500f, 500f);

        [Key(1)]
        public BedSideItem Bed { get; set; } = new BedSideItem();

        public SeaTruckSleeperModule Initialize(Action<NetworkDynamicEntityComponent> onEntityComponentInitialized)
        {
            onEntityComponentInitialized?.Invoke(this);
            return this;
        }
    }
}
