namespace Subnautica.Events.EventArgs
{
    using System;

    public class GrownPlantHarvestingEventArgs : EventArgs
    {
        public GrownPlantHarvestingEventArgs(string uniqueId, GrownPlant grownPlant, bool isAllowed = true)
        {
            this.UniqueId   = uniqueId;
            this.GrownPlant = grownPlant;
            this.IsAllowed  = isAllowed;
        }

        public string UniqueId { get; private set; }

        public GrownPlant GrownPlant { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
