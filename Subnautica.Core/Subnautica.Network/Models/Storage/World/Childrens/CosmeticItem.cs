namespace Subnautica.Network.Models.Storage.World.Childrens
{
    using MessagePack;

    using Subnautica.Network.Models.Metadata;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class CosmeticItem
    {
        [Key(0)]
        public StorageItem StorageItem { get; set; }

        [Key(1)]
        public string BaseId { get; set; }

        [Key(2)]
        public ZeroVector3 Position { get; set; }

        [Key(3)]
        public ZeroQuaternion Rotation { get; set; }

        public CosmeticItem()
        {

        }

        public CosmeticItem(StorageItem storageItem, string baseId, ZeroVector3 position, ZeroQuaternion rotation)
        {
            this.StorageItem = storageItem;
            this.BaseId      = baseId;
            this.Position    = position;
            this.Rotation    = rotation;
        }
    }
}
