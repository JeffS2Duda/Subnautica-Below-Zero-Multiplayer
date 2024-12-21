namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    [MessagePackObject]
    public class DeployableStorage : NetworkPlayerItemComponent
    {
        [Key(1)]
        public override TechType TechType { get; set; } = TechType.None;
        [Key(4)]
        public bool IsSignProcess { get; set; }

        [Key(5)]
        public bool IsSignSelect { get; set; }

        [Key(6)]
        public bool IsAdded { get; set; }


        [Key(7)]
        public string SignText { get; set; }

        [Key(8)]
        public int SignColorIndex { get; set; }

        [Key(9)]
        public WorldPickupItem WorldPickupItem { get; set; }
    }
}
