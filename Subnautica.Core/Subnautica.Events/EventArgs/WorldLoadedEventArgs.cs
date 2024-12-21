namespace Subnautica.Events.EventArgs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class WorldLoadedEventArgs : EventArgs
    {
        public WorldLoadedEventArgs()
        {
            this.WaitingMethods = new List<IEnumerator>();
        }

        public List<IEnumerator> WaitingMethods { get; set; }
    }
}
