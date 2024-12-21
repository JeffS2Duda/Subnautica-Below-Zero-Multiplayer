namespace Subnautica.Network.Models.Storage.Encyclopedia
{
    using MessagePack;

    using System;
    using System.Collections.Generic;

    [MessagePackObject]
    [Serializable]
    public class Encyclopedia
    {
        [Key(0)]
        public HashSet<string> Encyclopedias { get; set; } = new HashSet<string>();

        [Key(1)]
        public Dictionary<string, Dictionary<string, bool>> Players { get; set; } = new Dictionary<string, Dictionary<string, bool>>();
    }
}
