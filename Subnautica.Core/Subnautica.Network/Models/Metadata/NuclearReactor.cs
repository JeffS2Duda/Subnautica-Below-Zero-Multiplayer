namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;
    using Subnautica.Network.Core.Components;
    using System.Collections.Generic;

    [MessagePackObject]
    public class NuclearReactor : MetadataComponent
    {
        [Key(0)]
        public bool IsRemoving { get; set; } = false;

        [Key(1)]
        public List<TechType> Items { get; set; } = new List<TechType>();
    }
}
