namespace Subnautica.Network.Models.Storage.World
{
    using System;
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Metadata;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldStreamer;

    using WorldChildrens = Subnautica.Network.Models.Storage.World.Childrens;

    [MessagePackObject]
    [Serializable]
    public class World
    {
        [Key(0)]
        public double ServerTime { get; set; } = 400f;

        [Key(1)]
        public uint LastConstructionId { get; set; } = 0;

        [Key(2)]
        public bool IsFirstLogin { get; set; } = true;

        [Key(3)]
        public byte[] Constructions { get; set; }

        [Key(4)]
        public List<string> JukeboxDisks { get; set; } = new List<string>();

        [Key(5)]
        public HashSet<WorldChildrens.PowerSource> PowerSources { get; set; } = new HashSet<WorldChildrens.PowerSource>();

        [Key(6)]
        public Dictionary<string, NetworkWorldEntityComponent> PersistentEntities { get; set; } = new Dictionary<string, NetworkWorldEntityComponent>();

        [Key(7)]
        public HashSet<WorldDynamicEntity> DynamicEntities { get; set; } = new HashSet<WorldDynamicEntity>();

        [Key(8)]
        public float WorldSpeed { get; set; } = 1f;

        [Key(9)]
        public float TimeLastSleep { get; set; } = 0f;

        [Key(10)]
        public bool SkipTimeMode { get; set; } = false;

        [Key(11)]
        public float SkipModeEndTime { get; set; } = 0f;

        [Key(12)]
        public List<SupplyDrop> SupplyDrops { get; set; } = new List<SupplyDrop>();

        [Key(13)]
        public List<Base> Bases { get; set; } = new List<Base>();

        [Key(14)]
        public ushort LastItemId { get; set; }

        [Key(15)]
        public Metadata.StorageContainer QuantumLocker { get; set; } = Metadata.StorageContainer.Create(4, 4);

        [Key(16)]
        public Dictionary<string, string> SeaTruckConnections { get; set; } = new Dictionary<string, string>();

        [Key(17)]
        public HashSet<ZeroSpawnPointSimple> SpawnPoints { get; set; } = new HashSet<ZeroSpawnPointSimple>();

        [Key(18)]
        public bool IsWorldGenerated { get; set; } = false;

        [Key(19)]
        public List<string> ActivatedPrecursorTeleporters { get; set; } = new List<string>();

        [Key(20)]
        public HashSet<Brinicle> Brinicles { get; set; } = new HashSet<Brinicle>();

        [Key(21)]
        public HashSet<CosmeticItem> CosmeticItems { get; set; } = new HashSet<CosmeticItem>();

        [Key(22)]
        public HashSet<TechType> DiscoveredTechTypes { get; set; } = new HashSet<TechType>();
    }
}
