namespace Subnautica.Events.EventArgs
{
    using System;

    public class ScannerCompletedEventArgs : EventArgs
    {
        public ScannerCompletedEventArgs(TechType techType)
        {
            this.TechType = techType;
        }

        public TechType TechType { get; set; }
    }
}
