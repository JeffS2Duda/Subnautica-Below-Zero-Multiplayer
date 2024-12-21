namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Toilet : MetadataComponent
    {
        [Key(0)]
        public bool IsOpened { get; set; }

        public Toilet()
        {

        }

        public Toilet(bool isOpened)
        {
            this.IsOpened = isOpened;
        }
    }
}
