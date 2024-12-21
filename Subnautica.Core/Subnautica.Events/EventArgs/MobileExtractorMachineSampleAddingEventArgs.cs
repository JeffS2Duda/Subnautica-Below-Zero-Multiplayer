namespace Subnautica.Events.EventArgs
{
    using System;

    public class MobileExtractorMachineSampleAddingEventArgs : EventArgs
    {
        public MobileExtractorMachineSampleAddingEventArgs(bool isAllowed = true)
        {
            this.IsAllowed = isAllowed;
        }

        public bool IsAllowed { get; set; }
    }
}
