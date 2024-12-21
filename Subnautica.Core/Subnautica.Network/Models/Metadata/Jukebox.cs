namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Jukebox : MetadataComponent
    {
        [Key(0)]
        public string CurrentPlayingTrack { get; set; }

        [Key(1)]
        public bool IsPaused { get; set; }

        [Key(2)]
        public bool IsStoped { get; set; }

        [Key(3)]
        public bool IsNext { get; set; }

        [Key(4)]
        public bool IsPrevious { get; set; }

        [Key(5)]
        public global::Jukebox.Repeat RepeatMode { get; set; }

        [Key(6)]
        public bool IsShuffled { get; set; }

        [Key(7)]
        public float Position { get; set; }

        [Key(8)]
        public uint Length { get; set; }

        [Key(9)]
        public float Volume { get; set; }
    }
}
