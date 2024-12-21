namespace Subnautica.Events.EventArgs
{
    using System;

    public class TechnologyFragmentAddedEventArgs : EventArgs
    {
        public TechnologyFragmentAddedEventArgs(string uniqueId, TechType type, int unlocked, int totalFragment)
        {
            this.UniqueId      = uniqueId;
            this.TechType      = type;
            this.Unlocked      = unlocked;
            this.TotalFragment = totalFragment;
        }

        public string UniqueId { get; private set; }

        public TechType TechType { get; private set; }

        public int Unlocked { get; private set; }

        public int TotalFragment { get; private set; }
    }
}
