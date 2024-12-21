namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.WorldEntity;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    [MessagePackObject]
    public class StaticEntityPickedUpArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.StaticEntityPickedUp;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public StaticEntity Entity { get; set; }

        [Key(7)]
        public WorldPickupItem WorldPickupItem { get; set; }
    }
}
