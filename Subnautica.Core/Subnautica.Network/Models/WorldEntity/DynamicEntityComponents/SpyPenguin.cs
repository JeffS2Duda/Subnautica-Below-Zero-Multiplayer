namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    [MessagePackObject]
    public class SpyPenguin : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public Metadata.StorageContainer StorageContainer { get; set; } = Metadata.StorageContainer.Create(2, 2);

        [Key(1)]
        public string Name { get; set; } = null;

        [Key(2)]
        public LiveMixin LiveMixin { get; set; } = new LiveMixin(10f, 10f);
    }
}
