namespace Subnautica.Events.EventArgs
{
    using System;

    public class BedExitInUseModeEventArgs : EventArgs
    {
        public BedExitInUseModeEventArgs(string uniqueId, TechType techType, bool isSeaTruckModule)
        {
            this.UniqueId         = uniqueId;
            this.TechType         = techType;
            this.IsSeaTruckModule = isSeaTruckModule;
        }

        public string UniqueId { get; set; }

        public TechType TechType { get; set; }

        public bool IsSeaTruckModule { get; set; }
    }
}
