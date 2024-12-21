namespace Subnautica.Network.Models.Server
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class InventoryQuickSlotItemArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.InventoryQuickSlot;

        [Key(5)]
        public string[] Slots { get; set; }
        
        [Key(6)]
        public int ActiveSlot { get; set; }
    }
}
