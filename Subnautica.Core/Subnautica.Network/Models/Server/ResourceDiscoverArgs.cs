namespace Subnautica.Network.Models.Server
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class ResourceDiscoverArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.ResourceDiscover;

        [Key(5)]
        public TechType TechType { get; set; }

        [Key(6)]
        public List<string> MapRooms { get; set; } = new List<string>();
    }
}
