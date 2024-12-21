namespace Subnautica.Events.EventArgs
{
    using System;

    public class InGameMenuOpeningEventArgs : EventArgs
    {
        public InGameMenuOpeningEventArgs(bool isAllowed = true)
        {
            IsAllowed = isAllowed;
        }

        public bool IsAllowed { get; set; }
    }
}
