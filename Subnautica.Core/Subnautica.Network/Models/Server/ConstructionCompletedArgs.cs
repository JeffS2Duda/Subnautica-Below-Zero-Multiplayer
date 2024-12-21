namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class ConstructionCompletedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.ConstructingCompleted;
        
        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.Construction;

        [Key(3)]
        public override byte ChannelId { get; set; } = 1;

        [Key(5)]
        public uint Id { get; set; }

        [Key(6)]
        public string UniqueId { get; set; }

        [Key(7)]
        public string BaseId { get; set; }

        [Key(8)]
        public TechType TechType { get; set; }

        [Key(9)]
        public ZeroVector3 CellPosition { get; set; }

        [Key(10)]
        public ZeroVector3 LocalPosition { get; set; }

        [Key(11)]
        public ZeroQuaternion LocalRotation { get; set; }

        [Key(12)]
        public bool IsFaceHasValue { get; set; }

        [Key(13)]
        public Base.Direction FaceDirection { get; set; }

        [Key(14)]
        public Base.FaceType FaceType { get; set; }
    }
}
