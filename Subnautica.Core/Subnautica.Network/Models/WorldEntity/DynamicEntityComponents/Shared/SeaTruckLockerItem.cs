namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared
{
    using MessagePack;

    [MessagePackObject]
    public class SeaTruckLockerItem
    {
        [Key(0)]
        public string UniqueId { get; set; }

        [Key(1)]
        public Metadata.StorageContainer StorageContainer { get; set; }
    }
}
