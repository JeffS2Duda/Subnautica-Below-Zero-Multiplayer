namespace Subnautica.Network.Models.Server
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using System.Collections.Generic;

    [MessagePackObject]
    public class WorldDynamicEntityOwnershipChangedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.WorldDynamicEntityOwnershipChanged;

        [Key(5)]
        public Dictionary<string, List<ushort>> Entities { get; set; }
    }
}
