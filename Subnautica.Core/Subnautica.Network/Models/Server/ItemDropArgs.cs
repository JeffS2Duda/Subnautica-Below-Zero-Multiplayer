namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class ItemDropArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.ItemDrop;

        [Key(5)]
        public ZeroVector3 Forward { get; set; }

        [Key(6)]
        public WorldPickupItem WorldPickupItem { get; set; }

        [Key(7)]
        public WorldDynamicEntity Entity { get; set; }
    }
}
