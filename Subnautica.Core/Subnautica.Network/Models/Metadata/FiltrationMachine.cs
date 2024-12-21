namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Structures;
    using System.Collections.Generic;

    [MessagePackObject]
    public class FiltrationMachine : MetadataComponent
    {
        [Key(0)]
        public bool IsUnderwater { get; set; } = true;

        [Key(1)]
        public string RemovingItemId { get; set; }

        [Key(2)]
        public float TimeRemainingWater { get; set; } = 840f;

        [Key(3)]
        public float TimeRemainingSalt { get; set; } = 420f;

        [Key(4)]
        public FiltrationMachineItem Item { get; set; }

        [Key(6)]
        public List<FiltrationMachineItem> Items { get; set; } = new List<FiltrationMachineItem>()
        {
            new FiltrationMachineItem(TechType.BigFilteredWater),
            new FiltrationMachineItem(TechType.BigFilteredWater),
            new FiltrationMachineItem(TechType.Salt),
            new FiltrationMachineItem(TechType.Salt),
        };
    }

    [MessagePackObject]
    public class FiltrationMachineItem
    {
        [Key(0)]
        public TechType TechType { get; set; }

        [Key(1)]
        public string ItemId { get; set; } = null;

        public FiltrationMachineItem()
        {

        }

        public FiltrationMachineItem(TechType techType)
        {
            this.TechType = techType;
        }

        public void Clear()
        {
            this.ItemId = null;
        }
    }

    [MessagePackObject]
    public class FiltrationMachineTimeItem
    {
        [Key(0)]
        public uint ConstructionIndex { get; set; }

        [Key(1)]
        public float TimeRemainingWater { get; set; }

        [Key(2)]
        public float TimeRemainingSalt { get; set; }

        [IgnoreMember]
        public ZeroVector3 Position { get; set; }

        public FiltrationMachineTimeItem()
        {

        }

        public FiltrationMachineTimeItem(uint constructionIndex, float timeRemainingWater, float timeRemainingSalt, ZeroVector3 position)
        {
            this.ConstructionIndex = constructionIndex;
            this.TimeRemainingWater = timeRemainingWater;
            this.TimeRemainingSalt = timeRemainingSalt;
            this.Position = position;
        }
    }
}
