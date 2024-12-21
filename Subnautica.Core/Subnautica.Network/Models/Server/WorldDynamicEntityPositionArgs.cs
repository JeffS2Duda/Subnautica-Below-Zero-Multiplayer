namespace Subnautica.Network.Models.Server
{
    using System.Collections.Generic;

    using LiteNetLib;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.World.Childrens;

    [MessagePackObject]
    public class WorldDynamicEntityPositionArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.WorldDynamicEntityPosition;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.EntityMovement;

        [Key(2)]
        public override DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.Unreliable;

        [Key(5)]
        public List<WorldDynamicEntityPosition> Positions { get; set; }
    }
}
