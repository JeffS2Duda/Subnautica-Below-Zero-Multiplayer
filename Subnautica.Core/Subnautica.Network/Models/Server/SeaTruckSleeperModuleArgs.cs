namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.API.Features;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class SeaTruckSleeperModuleArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.SeaTruckSleeperModule;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public bool IsOpeningPictureFrame { get; set; }

        [Key(7)]
        public bool IsSelectingPictureFrame { get; set; }

        [Key(8)]
        public byte[] PictureFrameData { get; set; }

        [Key(9)]
        public string PictureFrameName { get; set; }

        [Key(10)]
        public CustomProperty JukeboxData { get; set; }

        [Key(11)]
        public Bed.BedSide SleepingSide { get; set; }

        [Key(12)]
        public bool IsSleeping { get; set; }
    }
}
