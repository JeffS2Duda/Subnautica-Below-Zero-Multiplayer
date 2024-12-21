namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    [MessagePackObject]
    public class SeaTruckStorageModuleArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.SeaTruckStorageModule;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public WorldPickupItem WorldPickupItem { get; set; }

        [Key(7)]
        public bool IsSignProcess { get; set; }

        [Key(8)]
        public bool IsSignSelect { get; set; }

        [Key(9)]
        public bool IsAdded { get; set; }

        [Key(10)]
        public string SignText { get; set; }

        [Key(11)]
        public int SignColorIndex { get; set; }
    }
}
