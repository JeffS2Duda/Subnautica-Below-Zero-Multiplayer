namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class WorldEntityActionArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.WorldEntityAction;

        [Key(5)]
        public NetworkWorldEntityComponent Entity { get; set; }
    }
}
