namespace Subnautica.Network.Models.Server
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using System.Collections.Generic;

    [MessagePackObject]
    public class StoryPlayerVisibilityArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.StoryPlayerVisibility;

        [Key(5)]
        public bool IsCinematicActive { get; set; }

        [Key(6)]
        public Dictionary<string, bool> Visibility { get; set; } = new Dictionary<string, bool>();
    }
}
