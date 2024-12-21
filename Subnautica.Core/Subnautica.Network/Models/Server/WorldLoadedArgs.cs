namespace Subnautica.Network.Models.Server
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class WorldLoadedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.WorldLoaded;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.Startup;

        [Key(5)]
        public bool IsSpawnPointRequest { get; set; }

        [Key(6)]
        public int SpawnPointCount { get; set; }

        [Key(7)]
        public List<string> Images { get; set; }
    }
}
