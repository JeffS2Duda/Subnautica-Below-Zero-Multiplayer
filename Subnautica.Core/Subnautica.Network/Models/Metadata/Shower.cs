namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Shower : MetadataComponent
    {
        [Key(0)]
        public bool IsActive { get; set; }

        public Shower()
        {

        }

        public Shower(bool isActive)
        {
            this.IsActive = isActive;
        }
    }
}
