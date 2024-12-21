namespace Subnautica.Events.EventArgs
{
    using System;

    public class BedIsCanSleepCheckingEventArgs : EventArgs
    {
        public BedIsCanSleepCheckingEventArgs(string uniqueId, Bed.BedSide side, bool isSeaTruckModule, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Side = side;
            this.IsAllowed = isAllowed;
            this.IsSeaTruckModule = isSeaTruckModule;
        }

        public string UniqueId { get; set; }

        public Bed.BedSide Side { get; set; }

        public bool IsSeaTruckModule { get; set; }

        public bool IsAllowed { get; set; }
    }
}
