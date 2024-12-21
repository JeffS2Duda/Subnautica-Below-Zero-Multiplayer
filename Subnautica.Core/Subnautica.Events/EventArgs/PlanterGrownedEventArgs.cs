namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class PlanterGrownedEventArgs : EventArgs
    {
        public PlanterGrownedEventArgs(FruitPlant fruitPlant)
        {
            this.FruitPlant = fruitPlant;
        }

        public FruitPlant FruitPlant { get; private set; }
    }
}
