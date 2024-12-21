namespace Subnautica.Events.EventArgs
{
    using System;

    public class MobileExtractorConsoleUsingEventArgs : EventArgs
    {
        public MobileExtractorConsoleUsingEventArgs(bool isAllowed = true)
        {
            this.IsAllowed = isAllowed;
        }

        public bool IsAllowed { get; set; }
    }
}
