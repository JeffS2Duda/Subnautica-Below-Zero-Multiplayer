namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class SeaTruckDockingModuleArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.SeaTruckDockingModule;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public string VehicleId { get; set; }

        [Key(7)]
        public WorldDynamicEntity Vehicle { get; set; }

        [Key(8)]
        public bool IsDocking { get; set; }

        [Key(9)]
        public bool IsEnterUndock { get; set; }

        [Key(10)]
        public ZeroVector3 UndockPosition { get; set; }

        [Key(11)]
        public ZeroQuaternion UndockRotation { get; set; }
    }
}
