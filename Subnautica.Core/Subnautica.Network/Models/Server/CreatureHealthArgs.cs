namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class CreatureHealthArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.CreatureHealth;

        [Key(5)]
        public ushort CreatureId { get; set; }

        [Key(6)]
        public bool IsDead { get; set; }

        [Key(7)]
        public float Damage { get; set; }

        [Key(8)]
        public DamageType DamageType { get; set; }
    }
}
