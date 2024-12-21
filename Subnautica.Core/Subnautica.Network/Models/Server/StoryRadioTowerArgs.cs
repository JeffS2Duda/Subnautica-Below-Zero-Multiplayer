namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class StoryRadioTowerArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.StoryRadioTower;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public bool IsTOMUsing { get; set; }
    }
}
