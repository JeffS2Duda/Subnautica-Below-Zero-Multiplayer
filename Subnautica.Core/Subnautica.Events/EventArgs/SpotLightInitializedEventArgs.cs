namespace Subnautica.Events.EventArgs
{
    using System;

    public class SpotLightInitializedEventArgs : EventArgs
    {
        public SpotLightInitializedEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; set; }
    }
}
