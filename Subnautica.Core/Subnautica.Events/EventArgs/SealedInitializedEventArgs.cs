namespace Subnautica.Events.EventArgs
{
    using System;

    public class SealedInitializedEventArgs : EventArgs
    {
        public SealedInitializedEventArgs(string uniqueId, Sealed sealedObject)
        {
            this.UniqueId = uniqueId;
            this.SealedObject = sealedObject;
        }

        public string UniqueId { get; private set; }

        public Sealed SealedObject { get; private set; }
    }
}
