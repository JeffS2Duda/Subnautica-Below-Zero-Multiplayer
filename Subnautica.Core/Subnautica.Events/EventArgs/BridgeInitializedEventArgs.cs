namespace Subnautica.Events.EventArgs
{
    using System;

    public class BridgeInitializedEventArgs : EventArgs
    {
        public BridgeInitializedEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; private set; }
    }
}
