namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class SmallStove : MetadataComponent
    {
        [Key(0)]
        public bool IsActive { get; set; }

        public SmallStove()
        {

        }

        public SmallStove(bool isActive)
        {
            this.IsActive = isActive;
        }
    }
}
