namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class BulkheadDoor : MetadataComponent
    {
        [Key(0)]
        public bool IsOpened { get; set; } = false;

        [Key(1)]
        public bool Side { get; set; } = false;

        public BulkheadDoor()
        {

        }

        public BulkheadDoor(bool isOpened, bool side)
        {
            this.IsOpened = isOpened;
            this.Side     = side;
        }
    }
}
