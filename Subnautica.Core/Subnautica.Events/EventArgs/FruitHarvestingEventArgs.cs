namespace Subnautica.Events.EventArgs
{
    using System;

    using Subnautica.API.Features;

    public class FruitHarvestingEventArgs : EventArgs
    {
        public FruitHarvestingEventArgs(PickPrefab pickPrefab, string uniqueId, TechType techType, byte maxSpawnableFruit, float spawnInterval, bool isAllowed = true)
        {
            this.PickPrefab          = pickPrefab;
            this.UniqueId            = uniqueId;
            this.TechType            = techType;
            this.MaxSpawnableFruit   = maxSpawnableFruit;
            this.SpawnInterval       = spawnInterval;
            this.IsAllowed           = isAllowed;
            this.IsStaticWorldEntity = Network.StaticEntity.IsStaticEntity(uniqueId);
        }

        public PickPrefab PickPrefab { get; set; }

        public string UniqueId { get; set; }

        public TechType TechType { get; set; }

        public byte MaxSpawnableFruit { get; set; }

        public float SpawnInterval { get; set; }

        public bool IsStaticWorldEntity { get; set; }

        public bool IsAllowed { get; set; }
    }
}
