namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    [MessagePackObject]
    public class SpawnOnKillArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.SpawnOnKill;

        [Key(5)]
        public WorldPickupItem WorldPickupItem { get; set; }

        [Key(6)]
        public WorldDynamicEntity Entity { get; set; }
    }
}
