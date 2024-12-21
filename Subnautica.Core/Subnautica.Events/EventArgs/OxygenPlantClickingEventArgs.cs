namespace Subnautica.Events.EventArgs
{
    using System;

    public class OxygenPlantClickingEventArgs : EventArgs
    {
        public OxygenPlantClickingEventArgs(string uniqueId, float startedTime)
        {
            this.UniqueId    = uniqueId;
            this.StartedTime = startedTime;
        }

        public string UniqueId { get; private set; }

        public float StartedTime { get; private set; }
    }
}
