namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class JukeboxDiskAddedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.JukeboxDiskAdded;

        [Key(5)]
        public string TrackFile { get; set; }

        [Key(6)]
        public bool Notify { get; set; }
    }
}
