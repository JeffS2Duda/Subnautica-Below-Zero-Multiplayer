namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class TechLight : MetadataComponent
    {
        [Key(0)]
        public bool IsPowered { get; set; }
    }
}
