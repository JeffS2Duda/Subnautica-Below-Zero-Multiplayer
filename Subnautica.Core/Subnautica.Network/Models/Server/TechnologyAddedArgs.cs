namespace Subnautica.Network.Models.Server
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class TechnologyAddedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.TechnologyAdded;

        [Key(5)]
        public TechType TechType { get; set; }

        [Key(6)]
        public bool Verbose { get; set; }
    }
}
