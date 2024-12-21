namespace Subnautica.Network.Models.Metadata
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    [MessagePackObject]
    public class Fridge : MetadataComponent
    {
        [Key(0)]
        public bool IsAdded { get; set; }

        [Key(1)]
        public bool IsPowerStateChanged { get; set; }

        [Key(2)]
        public bool WasPowered { get; set; }

        [Key(3)]
        public float CurrentTime { get; set; }

        [Key(4)]
        public bool IsDecomposes { get; set; }

        [Key(5)]
        public float TimeDecayStart { get; set; } = 0.0f;

        [Key(6)]
        public FridgeItemComponent ItemComponent { get; set; }

        [Key(7)]
        public WorldPickupItem WorldPickupItem { get; set; }

        [Key(8)]
        public Metadata.StorageContainer StorageContainer { get; set; }

        [Key(9)]
        public List<FridgeItemComponent> Components { get; set; } = new List<FridgeItemComponent>();
    }

    [MessagePackObject]
    public class FridgeItemComponent
    {
        [Key(0)]
        public string ItemId { get; set; }

        [Key(1)]
        public bool IsPaused { get; set; }

        [Key(2)]
        public bool IsDecomposes { get; set; }

        [Key(3)]
        public float TimeDecayPause { get; set; } = 0.0f;

        [Key(4)]
        public float TimeDecayStart { get; set; } = 0.0f;

        public void PauseDecay(float serverTime)
        {
            if (!this.IsPaused)
            {
                this.IsPaused = true;
                this.TimeDecayPause = serverTime;
            }
        }

        public void UnpauseDecay(float serverTime)
        {
            if (this.IsPaused)
            {
                this.IsPaused = false;
                this.TimeDecayStart += serverTime - this.TimeDecayPause;
           }
        }
    }
}
