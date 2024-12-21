namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;
    using System.Collections.Generic;

    [MessagePackObject]
    public class Charger : MetadataComponent
    {
        [Key(0)]
        public bool IsOpening { get; set; } = false;

        [Key(1)]
        public bool IsRemoving { get; set; } = false;

        [Key(2)]
        public bool IsClosing { get; set; } = false;

        [Key(3)]
        public List<BatteryItem> Items { get; set; } = new List<BatteryItem>();
    }

    [MessagePackObject]
    public class ChargerSimple
    {
        [Key(0)]
        public uint ConstructionId { get; set; }

        [Key(1)]
        public bool IsPowered { get; set; }

        [Key(2)]
        public bool IsCharging { get; set; }

        [Key(3)]
        public float[] Batteries { get; set; }

        [IgnoreMember]
        public ZeroVector3 Position { get; set; }

        public ChargerSimple()
        {

        }

        public ChargerSimple(uint constructionId, float[] items, bool isPowered, ZeroVector3 position)
        {
            this.ConstructionId = constructionId;
            this.Batteries = items;
            this.IsPowered = isPowered;
            this.Position = position;
        }
    }
}
