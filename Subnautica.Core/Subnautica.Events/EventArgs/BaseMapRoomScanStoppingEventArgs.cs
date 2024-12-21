namespace Subnautica.Events.EventArgs
{
    using System;

    public class BaseMapRoomScanStoppingEventArgs : EventArgs
    {
        public BaseMapRoomScanStoppingEventArgs(string uniqueId, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public bool IsAllowed { get; set; }
    }
}
