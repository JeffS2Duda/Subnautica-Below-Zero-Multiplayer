namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class AromatherapyLamp : MetadataComponent
    {
        [Key(0)]
        public bool IsActive { get; set; }

        public AromatherapyLamp()
        {

        }

        public AromatherapyLamp(bool isActive)
        {
            this.IsActive = isActive;
        }
    }
}
