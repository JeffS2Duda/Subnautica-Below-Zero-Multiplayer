namespace Subnautica.Events.EventArgs
{
    using System;

    public class SeaTruckModuleInitializedEventArgs : EventArgs
    {
        public SeaTruckModuleInitializedEventArgs(global::SeaTruckSegment module, TechType techType)
        {
            this.Module   = module;
            this.TechType = techType;
        }

        public global::SeaTruckSegment Module { get; set; }

        public TechType TechType { get; set; }
    }
}
