namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    using System.Collections.Generic;

    [MessagePackObject]
    public class ItemPinArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.ItemPin;

        [Key(5)]
        public List<TechType> Items { get; set; }
    }
}
