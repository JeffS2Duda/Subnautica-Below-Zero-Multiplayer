namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Recyclotron : MetadataComponent
    {
        [Key(0)]
        public bool IsRecycle { get; set; }
    }
}
