namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Metadata;

    [MessagePackObject]
    public class InventoryItemArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.InventoryItem;

        [Key(5)]
        public string ItemId { get; set; }

        [Key(6)]
        public StorageItem Item { get; set; }

        [Key(7)]
        public bool IsAdded { get; set; }
    }
}
