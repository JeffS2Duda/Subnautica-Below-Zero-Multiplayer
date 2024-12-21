namespace Subnautica.Events.EventArgs
{
    using System;

    public class ElevatorCallingEventArgs : EventArgs
    {
        public ElevatorCallingEventArgs(string uniqueId, bool isUp, bool isAllowed = true)
        {
            this.UniqueId  = uniqueId;
            this.IsUp      = isUp;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public bool IsUp { get; set; }

        public bool IsAllowed { get; set; }
    }
}
