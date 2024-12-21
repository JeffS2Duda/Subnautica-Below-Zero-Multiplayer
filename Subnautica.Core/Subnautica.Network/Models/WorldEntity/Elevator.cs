namespace Subnautica.Network.Models.WorldEntity
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Core.Components;

#pragma warning disable CS0612

    [MessagePackObject]
    public class Elevator : NetworkWorldEntityComponent
    {
        [Key(2)]
        public override EntityProcessType ProcessType { get; set; } = EntityProcessType.Elevator;

        [Key(4)]
        public bool IsUp { get; set; }

        [Key(5)]
        public float StartTime { get; set; }
    }
}
