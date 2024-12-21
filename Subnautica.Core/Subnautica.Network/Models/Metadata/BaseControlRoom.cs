namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class BaseControlRoom : MetadataComponent
    {
        [Key(0)]
        public string Name { get; set; }

        [Key(1)]
        public ZeroColor BaseColor { get; set; }

        [Key(2)]
        public ZeroColor StripeColor1 { get; set; }

        [Key(3)]
        public ZeroColor StripeColor2 { get; set; }

        [Key(4)]
        public ZeroColor NameColor { get; set; }

        [Key(5)]
        public BaseControlRoomMinimap Minimap { get; set; }

        [Key(6)]
        public bool IsNavigateOpening { get; set; }

        [Key(7)]
        public bool IsColorCustomizerOpening { get; set; }

        [Key(8)]
        public bool IsColorCustomizerSave { get; set; }

        [Key(9)]
        public bool IsNavigationExiting { get; set; }
    }

    [MessagePackObject]
    public class BaseControlRoomMinimap
    {
        [Key(0)]
        public ZeroVector3 Position { get; set; }

        [Key(1)]
        public ZeroInt3 Cell { get; set; }

        [Key(2)]
        public bool IsPowered { get; set; }

        public BaseControlRoomMinimap()
        {

        }

        public BaseControlRoomMinimap(ZeroVector3 position, ZeroInt3 cell, bool isPowered = false)
        {
            this.Position = position;
            this.Cell = cell;
            this.IsPowered = isPowered;
        }
    }
}
