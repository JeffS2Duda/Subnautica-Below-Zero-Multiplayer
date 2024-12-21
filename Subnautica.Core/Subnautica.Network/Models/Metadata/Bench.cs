namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Bench : MetadataComponent
    {
        [Key(0)]
        public global::Bench.BenchSide Side { get; set; }

        [Key(1)]
        public bool IsSitdown { get; set; }

        [Key(2)]
        public string PlayerId { get; set; }

        [Key(3)]
        public byte PlayerId_v2 { get; set; }

        public Bench()
        {

        }

        public Bench(global::Bench.BenchSide side, bool isSitdown)
        {
            this.Side      = side;
            this.IsSitdown = isSitdown;
        }

        public void Sitdown(byte playerId)
        {
            this.IsSitdown   = true;
            this.PlayerId    = null;
            this.PlayerId_v2 = playerId;
        }

        public void Standup()
        {
            this.IsSitdown   = false;
            this.PlayerId_v2 = 0;
            this.PlayerId    = null;
        }
    }
}
