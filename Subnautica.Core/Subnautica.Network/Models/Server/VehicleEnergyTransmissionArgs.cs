namespace Subnautica.Network.Models.Server
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using System.Collections.Generic;

    [MessagePackObject]
    public class VehicleEnergyTransmissionArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.VehicleEnergyTransmission;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.EnergyTransmission;

        [Key(5)]
        public List<VehicleEnergyTransmissionItem> PowerCells { get; set; }
    }

    [MessagePackObject]
    public class VehicleEnergyTransmissionItem
    {
        [Key(0)]
        public string VehicleId { get; set; }

        [Key(1)]
        public float PowerCell1 { get; set; }

        [Key(2)]
        public float PowerCell2 { get; set; }

        public VehicleEnergyTransmissionItem()
        {

        }

        public VehicleEnergyTransmissionItem(string vehicleId, float powerCell1, float powerCell2)
        {
            this.VehicleId = vehicleId;
            this.PowerCell1 = powerCell1;
            this.PowerCell2 = powerCell2;
        }
    }
}
