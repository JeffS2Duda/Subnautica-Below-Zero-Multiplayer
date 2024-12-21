namespace Subnautica.Network.Models.WorldEntity
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class BulkheadDoor : NetworkWorldEntityComponent
    {
        [Key(2)]
        public override EntityProcessType ProcessType { get; set; } = EntityProcessType.BulkheadDoor;

        [Key(4)]
        public bool Side { get; set; } = false;

        [Key(5)]
        public bool IsOpened { get; set; } = false;

        [Key(6)]
        public StoryCinematicType StoryCinematicType { get; set; } = StoryCinematicType.None;
    }
}
