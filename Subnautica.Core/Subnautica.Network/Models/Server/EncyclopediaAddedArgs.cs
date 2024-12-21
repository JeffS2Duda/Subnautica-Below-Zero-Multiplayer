namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class EncyclopediaAddedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.EncyclopediaAdded;

        [Key(5)]
        public string Key { get; set; }

        [Key(6)]
        public bool Verbose { get; set; }
    }
}
