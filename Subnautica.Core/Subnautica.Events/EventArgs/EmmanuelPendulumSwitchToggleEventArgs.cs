namespace Subnautica.Events.EventArgs
{
    using System;

    public class EmmanuelPendulumSwitchToggleEventArgs : EventArgs
    {
        public EmmanuelPendulumSwitchToggleEventArgs(string uniqueId, bool switchStatus, bool isAllowed = true)
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
