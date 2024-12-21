namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class SmallStorage : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public Metadata.StorageContainer StorageContainer { get; set; } = Metadata.StorageContainer.Create(4, 4);
    }
}
