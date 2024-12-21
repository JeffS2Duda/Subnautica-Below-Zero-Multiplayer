namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.API.Features;
    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class JukeboxUsed : MetadataComponent
    {
        [Key(0)]
        public CustomProperty Data { get; set; }
    }
}
