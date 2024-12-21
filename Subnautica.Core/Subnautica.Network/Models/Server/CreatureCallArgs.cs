namespace Subnautica.Network.Models.Server
{
    using LiteNetLib;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class CreatureCallArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.CreatureCallSound;

        [Key(2)]
        public override DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.Unreliable;

        [Key(5)]
        public ushort CreatureId { get; set; }

        [Key(6)]
        public byte CallId { get; set; }

        [Key(7)]
        public string Animation { get; set; }
    }
}
