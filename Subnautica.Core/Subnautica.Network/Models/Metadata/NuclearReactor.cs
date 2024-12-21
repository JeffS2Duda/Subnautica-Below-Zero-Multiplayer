namespace Subnautica.Network.Models.Metadata
{
    using System.Collections.Generic;

    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class NuclearReactor : MetadataComponent
    {
        [Key(0)]
        public bool IsRemoving { get; set; } = false;

        [Key(1)]
        public List<TechType> Items { get; set; } = new List<TechType>();
    }
}
