namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class PictureFrame : MetadataComponent
    {
        [Key(0)]
        public string ImageName { get; set; }

        [Key(1)]
        public byte[] ImageData { get; set; }

        [Key(2)]
        public bool IsOpening { get; set; }

        public PictureFrame()
        {

        }

        public PictureFrame(string imageName, byte[] imageData, bool isOpening)
        {
            this.ImageName = imageName;
            this.ImageData = imageData;
            this.IsOpening = isOpening;
        }
    }
}
