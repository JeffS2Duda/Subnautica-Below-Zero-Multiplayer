namespace Subnautica.Network.Models.Storage.Scanner
{
    using MessagePack;

    using System;
    using System.Collections.Generic;

    [MessagePackObject]
    [Serializable]
    public class Scanner
    {
        [Key(0)]
        public HashSet<TechType> Technologies { get; set; } = new HashSet<TechType>();
    }
}
