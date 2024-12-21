namespace Subnautica.Events.EventArgs
{
    using System;

    public class InGameMenuClosingEventArgs : EventArgs
    {
        public InGameMenuClosingEventArgs(bool isAllowed = true)
        {
            IsAllowed = isAllowed;
        }

        public bool IsAllowed { get; set; }
    }
}
