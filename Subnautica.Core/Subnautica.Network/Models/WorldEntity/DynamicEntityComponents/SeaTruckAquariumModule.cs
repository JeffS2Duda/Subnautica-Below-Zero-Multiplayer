namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using System;
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.API.Features;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    [MessagePackObject]
    public class SeaTruckAquariumModule : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public List<SeaTruckLockerItem> Lockers { get; set; } = new List<SeaTruckLockerItem>()
        {
            new SeaTruckLockerItem(),
            new SeaTruckLockerItem()
        };

        [Key(1)]
        public float LeftStorageTime { get; set; }

        [Key(2)]
        public float RightStorageTime { get; set; }

        [Key(3)]
        public LiveMixin LiveMixin { get; set; } = new LiveMixin(500f, 500f);

        public SeaTruckAquariumModule Initialize(Action<NetworkDynamicEntityComponent> onEntityComponentInitialized)
        {
            foreach (var locker in this.Lockers)
            {
                locker.UniqueId         = Network.Identifier.GenerateUniqueId();
                locker.StorageContainer = Metadata.StorageContainer.Create(2, 4);
            }

            onEntityComponentInitialized?.Invoke(this);
            return this;
        }
    }
}
