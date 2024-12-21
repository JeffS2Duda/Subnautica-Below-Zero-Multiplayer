namespace Subnautica.Events.EventArgs
{
    using System;

    public class AromatherapyLampSwitchToggleEventArgs : EventArgs
    {
        public AromatherapyLampSwitchToggleEventArgs(string uniqueId, bool switchStatus, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.SwitchStatus = switchStatus;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public bool SwitchStatus { get; set; }

        public bool IsAllowed { get; set; }
    }
}
