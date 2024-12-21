namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class StorageItem : MetadataComponent
    {
        [Key(0)]
        public string ItemId { get; set; }

        [Key(1)]
        public byte[] Item { get; set; }

        [Key(2)]
        public TechType TechType { get; set; }

        [Key(3)]
        public byte Size { get; set; }

        public void SetItem(TechType techType)
        {
            this.Item = null;
            this.Size = GetItemSize(techType);
            this.TechType = techType;
        }

        public byte GetSizeX()
        {
            return (byte)(this.Size % 10);
        }

        public byte GetSizeY()
        {
            return (byte)(this.Size / 10);
        }

        public static StorageItem Create(Pickupable pickupable, bool resetItem = false)
        {
            return new StorageItem()
            {
                ItemId = pickupable.gameObject.GetIdentityId(),
                Item = resetItem ? null : Serializer.SerializeGameObject(pickupable),
                TechType = pickupable.GetTechType(),
                Size = GetItemSize(pickupable.GetTechType()),
            };
        }

        public static StorageItem Create(string itemId, TechType techType)
        {
            return new StorageItem()
            {
                ItemId = itemId,
                TechType = techType,
                Size = GetItemSize(techType),
            };
        }

        public static StorageItem Create(TechType techType)
        {
            return StorageItem.Create(Network.Identifier.GenerateUniqueId(), techType);
        }

        private static byte GetItemSize(TechType techType)
        {
            var itemSize = TechData.GetItemSize(techType);

            return (byte)(itemSize.x + (itemSize.y * 10));
        }
    }
}
