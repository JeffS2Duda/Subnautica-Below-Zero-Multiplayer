namespace Subnautica.Network.Models.Server
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class VehicleRepairArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.VehicleRepair;

        [Key(5)]
        public List<VehicleRepairItem> Repairs { get; set; } = new List<VehicleRepairItem>();
    }

    [MessagePackObject]
    public class VehicleRepairItem
    {
        [Key(0)]
        public string VehicleId { get; set; }

        [Key(1)]
        public float Health { get; set; }

        public VehicleRepairItem()
        {

        }

        public VehicleRepairItem(string vehicleId, float health)
        {
            this.VehicleId = vehicleId;
            this.Health    = health;
        }
    }
}
