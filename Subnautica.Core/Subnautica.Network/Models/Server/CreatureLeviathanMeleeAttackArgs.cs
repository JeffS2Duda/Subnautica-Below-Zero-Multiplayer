namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class CreatureLeviathanMeleeAttackArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.CreatureLeviathanMeleeAttack;

        [Key(5)]
        public ushort CreatureId { get; set; }

        [Key(6)]
        public float BiteDamage { get; set; }

        [Key(7)]
        public double ProcessTime { get; set; }

        [Key(8)]
        public ZeroLastTarget Target { get; set; }
    }
}
