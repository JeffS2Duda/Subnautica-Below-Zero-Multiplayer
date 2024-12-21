namespace Subnautica.Events.EventArgs
{
    using System;

    public class JukeboxDiskPickedUpEventArgs : EventArgs
    {
        public JukeboxDiskPickedUpEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; set; }
    }
}
