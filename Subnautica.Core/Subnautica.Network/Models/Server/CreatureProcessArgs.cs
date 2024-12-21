namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class CreatureProcessArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.CreatureProcess;

        [Key(5)]
        public double ProcessTime { get; set; }

        [Key(6)]
        public ushort CreatureId { get; set; }

        [Key(7)]
        public TechType CreatureType { get; set; }

        [Key(8)]
        public NetworkCreatureComponent Component { get; set; }
    }
}
