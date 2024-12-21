namespace Subnautica.Events.EventArgs
{
    using System;

    public class InventoryItemRemovedEventArgs : EventArgs
    {
        public InventoryItemRemovedEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; set; }
    }
}
