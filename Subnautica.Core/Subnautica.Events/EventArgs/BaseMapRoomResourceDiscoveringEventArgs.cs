namespace Subnautica.Events.EventArgs
{
    using System;

    public class BaseMapRoomResourceDiscoveringEventArgs : EventArgs
    {
        public BaseMapRoomResourceDiscoveringEventArgs(TechType techType, bool isAllowed = true)
        {
            this.TechType  = techType;
            this.IsAllowed = isAllowed;
        }

        public TechType TechType { get; set; }

        public bool IsAllowed { get; set; }
    }
}
