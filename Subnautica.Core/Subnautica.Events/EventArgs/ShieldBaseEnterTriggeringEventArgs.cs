namespace Subnautica.Events.EventArgs
{
    using System;

    public class ShieldBaseEnterTriggeringEventArgs : EventArgs
    {
        public ShieldBaseEnterTriggeringEventArgs(bool isAllowed = true)
        {
            this.IsAllowed = isAllowed;
        }

        public bool IsAllowed { get; set; }
    }
}
