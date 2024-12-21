namespace Subnautica.Network.Models.WorldStreamer
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.Network.Models.WorldEntity;

    [MessagePackObject]
    public class ZeroSpawnPointContainer
    {
        [Key(0)]
        public int TotalSpawnPoint { get; set; }

        [Key(1)]
        public HashSet<int> ActiveSlots { get; set; } = new HashSet<int>();

        [Key(2)]
        public Dictionary<int, ZeroSpawnPoint> SpawnPoints { get; set; } = new Dictionary<int, ZeroSpawnPoint>();
    }
}
