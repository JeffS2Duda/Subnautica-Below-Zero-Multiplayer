namespace Subnautica.Events.EventArgs
{
    using System;

    public class TeleporterTerminalActivatingEventArgs : EventArgs
    {
        public TeleporterTerminalActivatingEventArgs(string uniqueId, string teleporterId, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.TeleporterId = teleporterId;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public string TeleporterId { get; set; }

        public bool IsAllowed { get; set; }
    }
}
