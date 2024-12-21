namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class PlanterProgressCompletedEventArgs : EventArgs
    {
        public PlanterProgressCompletedEventArgs(Plantable plantable, GameObject grownPlant)
        {
            this.Plantable  = plantable;
            this.GrownPlant = grownPlant;
        }

        public Plantable Plantable { get; private set; }

        public GameObject GrownPlant { get; private set; }
    }
}

