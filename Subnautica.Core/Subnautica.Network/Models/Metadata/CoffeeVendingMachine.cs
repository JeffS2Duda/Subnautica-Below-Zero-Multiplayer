namespace Subnautica.Network.Models.Metadata
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    [MessagePackObject]
    public class CoffeeVendingMachine : MetadataComponent
    {
        [Key(0)]
        public bool IsAdding { get; set; }

        [Key(1)]
        public bool IsFull { get; set; }

        [Key(2)]
        public bool WasPowered { get; set; }

        [Key(3)]
        public string ItemId { get; set; }

        [Key(4)]
        public List<CoffeeThermos> Thermoses { get; set; } = new List<CoffeeThermos> ()
        { 
            new CoffeeThermos(),
            new CoffeeThermos(),
        };

        [Key(5)]
        public WorldPickupItem PickupItem { get; set; }
    }

    [MessagePackObject]
    public class CoffeeThermos
    {
        [Key(0)]
        public bool IsActive { get; set; } = false;

        [Key(1)]
        public string ItemId { get; set; } = null;

        [Key(2)]
        public bool IsFull { get; set; } = false;

        [Key(3)]
        public float AddedTime { get; set; } = 0.0f;

        public void Initialize(string itemId, float currentTime)
        {
            this.IsActive  = true;
            this.IsFull    = false;
            this.ItemId    = itemId;
            this.AddedTime = currentTime;
        }

        public void Refill()
        {
            this.IsFull = true;
        }

        public void Clear()
        {
            this.IsActive  = false;
            this.IsFull    = false;
            this.ItemId    = null;
            this.AddedTime = 0.0f;
        }
    }
}
