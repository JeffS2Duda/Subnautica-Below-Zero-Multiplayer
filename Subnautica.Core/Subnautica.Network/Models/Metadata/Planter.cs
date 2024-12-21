namespace Subnautica.Network.Models.Metadata
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Planter : MetadataComponent
    {
        [Key(0)]
        public bool IsOpening { get; set; }

        [Key(1)]
        public bool IsAdding { get; set; }

        [Key(2)]
        public bool IsHarvesting { get; set; }

        [Key(3)]
        public PlanterItem CurrentItem { get; set; }

        [Key(4)]
        public List<PlanterItem> Items { get; set; } = new List<PlanterItem>();
    }

    [MessagePackObject]
    public class PlanterItem
    {
        [Key(0)]
        public TechType TechType { get; set; }

        [Key(1)]
        public string ItemId { get; set; }

        [Key(2)]
        public byte SlotId { get; set; }

        [Key(3)]
        public float TimeStartGrowth { get; set; } = -1f;

        [Key(4)]
        public float Health { get; set; } = -1f;

        [Key(5)]
        public short Duration { get; set; } = -1;

        [Key(6)]
        public byte ActiveFruitCount { get; set; } = 0;

        [Key(7)]
        public float TimeNextFruit { get; set; } = 0f;

        [Key(8)]
        public byte MaxSpawnableFruit { get; set; } = 0;

        [Key(9)]
        public float FruitSpawnInterval { get; set; } = 0f;

        public void SyncFruits(float currentTime)
        {
            while (this.ActiveFruitCount < this.MaxSpawnableFruit && currentTime >= this.TimeNextFruit)
            {
                this.TimeNextFruit += this.FruitSpawnInterval;

                this.ActiveFruitCount++;
            }

            if (this.ActiveFruitCount >= this.MaxSpawnableFruit)
            {
                this.TimeNextFruit = currentTime + this.FruitSpawnInterval;
            }
        }
    }
}
