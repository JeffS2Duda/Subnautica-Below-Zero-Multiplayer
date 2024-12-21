namespace Subnautica.Network.Models.Storage.Player
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class PlayerItem
    {
        [Key(0)]
        public string UniqueId { get; set; }

        [Key(1)]
        public byte PlayerId { get; set; }

        [Key(2)]
        public string PlayerName { get; set; }

        [Key(3)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(4)]
        public ZeroVector3 Position { get; set; }

        [Key(5)]
        public string SubrootId { get; set; }

        [Key(6)]
        public string InteriorId { get; set; }
    }
}
