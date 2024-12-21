namespace Subnautica.Network.Models.WorldEntity
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    [MessagePackObject]
    public class DestroyableDynamicEntity : NetworkWorldEntityComponent
    {
        [Key(2)]
        public override EntityProcessType ProcessType { get; set; } = EntityProcessType.DestroyableDynamic;

        [Key(4)]
        public WorldPickupItem PickupItem { get; set; }

        [Key(5)]
        public bool IsWorldStreamer { get; set; }
    }
}
