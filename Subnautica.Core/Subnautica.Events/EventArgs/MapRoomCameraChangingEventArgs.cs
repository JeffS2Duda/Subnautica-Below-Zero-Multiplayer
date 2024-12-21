namespace Subnautica.Events.EventArgs
{
    using System;

    public class MapRoomCameraChangingEventArgs : EventArgs
    {
        public MapRoomCameraChangingEventArgs(string uniqueId, bool isNext, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.IsNext = isNext;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public bool IsNext { get; set; }

        public bool IsAllowed { get; set; }
    }
}
