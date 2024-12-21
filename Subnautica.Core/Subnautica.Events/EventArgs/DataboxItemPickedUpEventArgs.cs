namespace Subnautica.Events.EventArgs
{
    using System;

    public class DataboxItemPickedUpEventArgs : EventArgs
    {
        public DataboxItemPickedUpEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; set; }
    }
}
