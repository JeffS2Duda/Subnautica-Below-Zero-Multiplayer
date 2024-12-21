namespace Subnautica.Network.Models.WorldEntity
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class OxygenPlant : NetworkWorldEntityComponent
    {
        [Key(2)]
        public override EntityProcessType ProcessType { get; set; } = EntityProcessType.OxygenPlant;

        [Key(4)]
        public float StartedTime { get; set; }
    }
}
