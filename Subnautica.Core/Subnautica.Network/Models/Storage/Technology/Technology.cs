namespace Subnautica.Network.Models.Storage.Technology
{
    using MessagePack;
    using System;
    using System.Collections.Generic;

    [MessagePackObject]
    [Serializable]
    public class Technology
    {
        [Key(0)]
        public Dictionary<TechType, TechnologyItem> Technologies { get; set; } = new Dictionary<TechType, TechnologyItem>();

        [Key(1)]
        public HashSet<TechType> AnalizedTechnologies { get; set; } = new HashSet<TechType>();
    }
}
