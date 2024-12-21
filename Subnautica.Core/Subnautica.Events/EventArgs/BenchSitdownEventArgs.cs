namespace Subnautica.Events.EventArgs
{
    using System;

    public class BenchSitdownEventArgs : EventArgs
    {
        public BenchSitdownEventArgs(string uniqueId, Bench.BenchSide side, TechType techType, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Side = side;
            this.TechType = techType;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public Bench.BenchSide Side { get; set; }

        public TechType TechType { get; set; }

        public bool IsAllowed { get; set; }
    }
}
