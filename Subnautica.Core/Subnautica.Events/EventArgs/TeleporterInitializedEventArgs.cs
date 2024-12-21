namespace Subnautica.Events.EventArgs
{
    using System;

    public class TeleporterInitializedEventArgs : EventArgs
    {
        public TeleporterInitializedEventArgs(string uniqueId, string teleporterId, bool isExit)
        {
            this.UniqueId = uniqueId;
            this.TeleporterId = teleporterId;
            this.IsExit = isExit;
        }

        public string UniqueId { get; set; }

        public string TeleporterId { get; set; }

        public bool IsExit { get; set; }
    }
}
