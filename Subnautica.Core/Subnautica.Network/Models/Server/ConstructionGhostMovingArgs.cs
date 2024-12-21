
namespace Subnautica.Network.Models.Server
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Construction.Shared;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class ConstructionGhostMovingArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.ConstructingGhostMoving;
        
        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.Construction;

        [Key(3)]
        public override byte ChannelId { get; set; } = 1;

        [Key(5)]
        public TechType TechType { get; set; }

        [Key(6)]
        public string UniqueId { get; set; }

        [Key(7)]
        public ZeroVector3 Position { get; set; }

        [Key(8)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(9)]
        public ZeroTransform AimTransform { get; set; }

        [Key(10)]
        public BaseGhostComponent BaseGhostComponent { get; set; }

        [Key(11)]
        public bool IsCanPlace { get; set; }

        [Key(12)]
        public bool UpdatePlacement { get; set; }

        [Key(13)]
        public int LastRotation { get; set; }
    }
}
