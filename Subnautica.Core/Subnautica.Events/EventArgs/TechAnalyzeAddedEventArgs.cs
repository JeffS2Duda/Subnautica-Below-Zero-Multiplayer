namespace Subnautica.Events.EventArgs
{
    using System;

    public class TechAnalyzeAddedEventArgs : EventArgs
    {
        public TechAnalyzeAddedEventArgs(TechType techType, bool verbose)
        {
            this.TechType = techType;
            this.Verbose = verbose;
        }

        public TechType TechType { get; private set; }

        public bool Verbose { get; private set; }
    }
}
