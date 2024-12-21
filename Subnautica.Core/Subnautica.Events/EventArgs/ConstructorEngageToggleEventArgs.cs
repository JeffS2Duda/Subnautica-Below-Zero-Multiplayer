namespace Subnautica.Events.EventArgs
{
    using System;

    public class ConstructorEngageToggleEventArgs : EventArgs
    {
        public ConstructorEngageToggleEventArgs(string uniqueId, bool isEngage, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.IsEngage = isEngage;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public bool IsEngage { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
