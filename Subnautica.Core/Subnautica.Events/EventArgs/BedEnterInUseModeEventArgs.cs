namespace Subnautica.Events.EventArgs
{
    using System;

    public class BedEnterInUseModeEventArgs : EventArgs
    {
        public BedEnterInUseModeEventArgs(string uniqueId, Bed.BedSide side, TechType techType, bool isSeaTruckModule, bool isAllowed = true)
        {
            this.UniqueId         = uniqueId;
            this.Side             = side;
            this.TechType         = techType;
            this.IsAllowed        = isAllowed;
            this.IsSeaTruckModule = isSeaTruckModule;
        }

        public string UniqueId { get; set; }

        public Bed.BedSide Side { get; set; }

        public TechType TechType { get; set; }

        public bool IsSeaTruckModule { get; set; }

        public bool IsAllowed { get; set; }
    }
}
