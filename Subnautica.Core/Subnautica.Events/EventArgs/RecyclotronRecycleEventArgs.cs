namespace Subnautica.Events.EventArgs
{
    using System;

    public class RecyclotronRecycleEventArgs : EventArgs
    {
        public RecyclotronRecycleEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; set; }
    }
}
