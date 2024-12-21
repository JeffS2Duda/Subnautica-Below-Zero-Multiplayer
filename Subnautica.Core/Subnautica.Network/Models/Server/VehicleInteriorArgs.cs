namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class VehicleInteriorArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.VehicleInterior;

        [Key(5)]
        public string VehicleId { get; set; }

        [Key(6)]
        public bool IsEntered { get; set; }
    }
}
