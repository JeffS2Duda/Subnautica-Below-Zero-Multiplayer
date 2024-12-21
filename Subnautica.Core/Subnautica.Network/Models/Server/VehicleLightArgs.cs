namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class VehicleLightArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.VehicleLight;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public bool IsActive { get; set; }

        [Key(7)]
        public TechType TechType { get; set; }
    }
}
