namespace Subnautica.Events.EventArgs
{
    using System;

    public class TechnologyAddedEventArgs : EventArgs
    {
        public TechnologyAddedEventArgs(TechType type, bool verbose)
        {
            this.TechType = type;
            this.Verbose  = verbose;
        }

        public TechType TechType { get; private set; }

        public bool Verbose { get; private set; }
    }
}
