namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class EmmanuelPendulum : MetadataComponent
    {
        [Key(0)]
        public bool IsActive { get; set; }

        public EmmanuelPendulum()
        {

        }

        public EmmanuelPendulum(bool isActive)
        {
            this.IsActive = isActive;
        }
    }
}
