namespace Subnautica.Events.EventArgs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class WorldLoadingEventArgs : EventArgs
    {
        public WorldLoadingEventArgs(IEnumerator method = null)
        {
            this.WaitingMethods = new List<IEnumerator>();
        }

        public List<IEnumerator> WaitingMethods { get; set; }
    }
}
