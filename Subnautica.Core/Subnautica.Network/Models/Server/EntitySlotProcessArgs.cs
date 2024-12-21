namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class EntitySlotProcessArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.EntitySlotProcess;

        [Key(5)]
        public bool IsBreakable { get; set; }

        [Key(6)]
        public ZeroVector3 Position { get; set; }

        [Key(7)]
        public WorldDynamicEntity Entity { get; set; }

        [Key(8)]
        public WorldPickupItem WorldPickupItem { get; set; }
    }
}
