namespace Subnautica.Events.EventArgs
{
    using System;
    using System.Collections.Generic;

    public class AquariumDataChangedEventArgs : EventArgs
    {
        public AquariumDataChangedEventArgs(string uniqueId, List<TechType> fishes)
        {
            this.UniqueId = uniqueId;
            this.Fishes = fishes;
        }

        public string UniqueId { get; private set; }

        public List<TechType> Fishes { get; private set; }
    }
}
