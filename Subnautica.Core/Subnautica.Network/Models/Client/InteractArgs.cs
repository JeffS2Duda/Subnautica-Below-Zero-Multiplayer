namespace Subnautica.Network.Models.Client
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using System.Collections.Generic;

    [MessagePackObject]
    public class InteractArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.Interact;

        [Key(5)]
        public Dictionary<string, string> List { get; set; }
    }
}
