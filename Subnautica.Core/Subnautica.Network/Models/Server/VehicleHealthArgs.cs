namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class VehicleHealthArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.VehicleHealth;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public float Damage { get; set; }

        [Key(7)]
        public float NewHealth { get; set; }

        [Key(8)]
        public DamageType DamageType { get; set; }
    }
}
