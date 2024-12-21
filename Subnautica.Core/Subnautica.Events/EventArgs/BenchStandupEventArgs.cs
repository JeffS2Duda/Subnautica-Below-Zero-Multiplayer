namespace Subnautica.Events.EventArgs
{
    using System;

    public class BenchStandupEventArgs : EventArgs
    {
        public BenchStandupEventArgs(string uniqueId, Bench.BenchSide side, TechType techType)
        {
            this.UniqueId = uniqueId;
            this.Side = side;
            this.TechType = techType;
        }

        public string UniqueId { get; set; }

        public Bench.BenchSide Side { get; set; }

        public TechType TechType { get; set; }
    }
}
