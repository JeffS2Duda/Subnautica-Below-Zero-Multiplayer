namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    [MessagePackObject]
    public class BioReactor : MetadataComponent
    {
        [Key(0)]
        public bool IsAdded { get; set; }

        [Key(1)]
        public WorldPickupItem WorldPickupItem { get; set; }

        [Key(2)]
        public StorageContainer StorageContainer { get; set; } = Metadata.StorageContainer.Create(4, 4);
    }
}
