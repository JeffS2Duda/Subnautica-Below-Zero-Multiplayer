namespace Subnautica.Network.Models.Server
{
    using LiteNetLib;
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using System.Collections.Generic;

    [MessagePackObject]
    public class WorldCreaturePositionArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.WorldCreaturePosition;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.FishMovement;

        [Key(2)]
        public override DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.Unreliable;

        [Key(5)]
        public List<WorldCreaturePosition> Positions { get; set; } = new List<WorldCreaturePosition>();
    }

    [MessagePackObject]
    public class WorldCreaturePosition
    {
        [Key(0)]
        public ushort CreatureId { get; set; }

        [Key(1)]
        public long Position { get; set; }

        [Key(2)]
        public long Rotation { get; set; }

        public WorldCreaturePosition()
        {

        }

        public WorldCreaturePosition(ushort creatureId, long position, long rotation)
        {
            this.CreatureId = creatureId;
            this.Position = position;
            this.Rotation = rotation;
        }
    }
}
