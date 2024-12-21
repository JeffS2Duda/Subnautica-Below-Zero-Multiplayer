namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class InteriorToggleArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.InteriorToggle;

        [Key(5)]
        public string InteriorId { get; set; }

        [Key(6)]
        public bool IsEntered { get; set; }
    }
}
