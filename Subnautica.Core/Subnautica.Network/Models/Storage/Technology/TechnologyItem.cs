namespace Subnautica.Network.Models.Storage.Technology
{
    using MessagePack;
    using System.Collections.Generic;

    [MessagePackObject]
    public class TechnologyItem
    {
        [Key(0)]
        public TechType TechType { get; set; } = TechType.None;

        [Key(1)]
        public int TotalFragment { get; set; } = 0;

        [Key(2)]
        public int Unlocked { get; set; } = 0;

        [Key(3)]
        public HashSet<string> Fragments { get; set; } = new HashSet<string>();
    }
}
