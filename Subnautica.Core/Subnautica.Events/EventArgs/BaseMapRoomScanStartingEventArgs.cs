namespace Subnautica.Events.EventArgs
{
    using System;

    public class BaseMapRoomScanStartingEventArgs : EventArgs
    {
        public BaseMapRoomScanStartingEventArgs(string uniqueId, TechType scanType, bool isAllowed = true)
        {
            this.UniqueId  = uniqueId;
            this.ScanType  = scanType;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public TechType ScanType { get; set; }

        public bool IsAllowed { get; set; }
    }
}
