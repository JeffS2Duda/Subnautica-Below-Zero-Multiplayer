namespace Subnautica.Events.EventArgs
{
    using System;

    public class BridgeTerminalClickingEventArgs : EventArgs
    {
        public BridgeTerminalClickingEventArgs(string uniqueId, bool isExtend, double time, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.IsExtend = isExtend;
            this.Time = (float)time;
        }

        public string UniqueId { get; private set; }

        public bool IsExtend { get; set; }

        public float Time { get; set; }

        public bool IsAllowed { get; set; }
    }
}
