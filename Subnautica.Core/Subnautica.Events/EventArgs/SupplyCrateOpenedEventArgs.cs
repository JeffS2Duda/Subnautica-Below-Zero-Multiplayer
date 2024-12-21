namespace Subnautica.Events.EventArgs
{
    using System;

    public class SupplyCrateOpenedEventArgs : EventArgs
    {
        public SupplyCrateOpenedEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; private set; }
    }
}
