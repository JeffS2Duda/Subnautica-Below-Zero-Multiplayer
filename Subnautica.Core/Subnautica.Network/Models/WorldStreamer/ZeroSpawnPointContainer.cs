namespace Subnautica.Network.Models.WorldStreamer
{
    using MessagePack;
    using System.Collections.Generic;

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
