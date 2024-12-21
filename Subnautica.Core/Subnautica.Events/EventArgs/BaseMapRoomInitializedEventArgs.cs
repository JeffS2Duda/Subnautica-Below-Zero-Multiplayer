namespace Subnautica.Events.EventArgs
{
    using System;

    public class BaseMapRoomInitializedEventArgs : EventArgs
    {
        public BaseMapRoomInitializedEventArgs(uGUI_MapRoomScanner mapRoom)
        {
            this.MapRoom = mapRoom;
        }

        public uGUI_MapRoomScanner MapRoom { get; set; }
    }
}
