namespace Subnautica.Events.EventArgs
{
    using System;

    public class SpyPenguinSnowStalkerInteractingEventArgs : EventArgs
    {
        public SpyPenguinSnowStalkerInteractingEventArgs(string uniqueId, float spawnChance, bool isAllowed = true)
        {
            this.UniqueId    = uniqueId;
            this.SpawnChance = spawnChance;
            this.IsAllowed   = isAllowed;
        }

        public string UniqueId { get; set; }

        public float SpawnChance { get; set; }

        public bool IsAllowed { get; set; }
    }
}
