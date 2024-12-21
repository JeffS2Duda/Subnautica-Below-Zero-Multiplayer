namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class PlayerFreezeArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.PlayerFreeze;

        [Key(5)]
        public bool IsFreeze { get; set; }

        [Key(6)]
        public float EndTime { get; set; }
    }
}
