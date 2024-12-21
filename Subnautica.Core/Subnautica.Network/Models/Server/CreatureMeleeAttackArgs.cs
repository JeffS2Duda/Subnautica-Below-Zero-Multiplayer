namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class CreatureMeleeAttackArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.CreatureMeleeAttack;

        [Key(5)]
        public ushort CreatureId { get; set; }

        [Key(6)]
        public float BiteDamage { get; set; }

        [Key(7)]
        public ZeroLastTarget Target { get; set; }
    }
}
