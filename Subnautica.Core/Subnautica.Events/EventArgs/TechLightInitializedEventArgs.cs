namespace Subnautica.Events.EventArgs
{
    using System;

    public class TechLightInitializedEventArgs : EventArgs
    {
        public TechLightInitializedEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; set; }
    }
}
