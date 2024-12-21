namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class Hoverpad : MetadataComponent
    {
        [Key(0)]
        public bool IsSpawning { get; set; }

        [Key(1)]
        public float FinishedTime { get; set; }

        [Key(2)]
        public bool IsDocked { get; set; }

        [Key(3)]
        public string ItemId { get; set; }

        [Key(4)]
        public bool IsDocking { get; set; }

        [Key(5)]
        public bool IsUnDocking { get; set; }

        [Key(6)]
        public bool IsCustomizerOpening { get; set; }

        [Key(7)]
        public byte ShowroomTriggerType { get; set; }

        [Key(8)]
        public byte ShowroomPlayerCount { get; set; }

        [Key(9)]
        public ZeroVector3 HoverbikePosition { get; set; }

        [Key(10)]
        public ZeroQuaternion HoverbikeRotation { get; set; }

        [Key(11)]
        public ZeroColorCustomizer ColorCustomizer { get; set; }

        [Key(12)]
        public Hoverbike Hoverbike { get; set; }

        [Key(13)]
        public WorldDynamicEntity Entity { get; set; }
    }
}
