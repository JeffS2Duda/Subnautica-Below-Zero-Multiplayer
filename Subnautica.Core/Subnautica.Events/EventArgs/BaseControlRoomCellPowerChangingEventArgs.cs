namespace Subnautica.Events.EventArgs
{
    using System;

    public class BaseControlRoomCellPowerChangingEventArgs : EventArgs
    {
        public BaseControlRoomCellPowerChangingEventArgs(string uniqueId, Int3 cell, bool isAllowed = true)
        {
            this.UniqueId  = uniqueId;
            this.Cell      = cell;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public Int3 Cell { get; set; }

        public bool IsAllowed { get; set; }
    }
}
