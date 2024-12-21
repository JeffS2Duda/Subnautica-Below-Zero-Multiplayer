namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class VehicleBatteryArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.VehicleBattery;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public string BatterySlotId { get; set; }

        [Key(7)]
        public TechType BatteryType { get; set; }

        [Key(8)]
        public bool IsOpening { get; set; }

        [Key(9)]
        public bool IsAdding { get; set; }

        [Key(10)]
        public float Charge { get; set; }
    }
}
