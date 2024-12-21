namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Sink : MetadataComponent
    {
        [Key(0)]
        public bool IsActive { get; set; }

        public Sink()
        {

        }

        public Sink(bool isActive)
        {
            this.IsActive = isActive;
        }
    }
}
