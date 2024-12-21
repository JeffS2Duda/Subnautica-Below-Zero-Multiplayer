namespace Subnautica.Network.Models.Server
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.World.Childrens;

    [MessagePackObject]
    public class BrinicleArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.Brinicle;

        [Key(5)]
        public List<Brinicle> WaitingForRegistry { get; set; } = new List<Brinicle>();

        [Key(6)]
        public List<Brinicle> Brinicles { get; set; } = new List<Brinicle>();

        [Key(7)]
        public string UniqueId { get; set; }

        [Key(8)]
        public float Damage { get; set; } 
    }
}
