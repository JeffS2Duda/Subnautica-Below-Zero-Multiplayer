namespace Subnautica.Events.EventArgs
{
    using System;

    public class HoverpadHoverbikeSpawningEventArgs : EventArgs
    {
        public HoverpadHoverbikeSpawningEventArgs(string uniqueId, bool isAllowed = true)
        {
            this.UniqueId  = uniqueId;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
