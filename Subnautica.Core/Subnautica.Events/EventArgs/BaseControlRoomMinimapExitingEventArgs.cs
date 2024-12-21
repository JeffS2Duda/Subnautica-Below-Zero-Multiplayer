namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class BaseControlRoomMinimapExitingEventArgs : EventArgs
    {
        public BaseControlRoomMinimapExitingEventArgs(string uniqueId, Vector3 mapPosition)
        {
            this.UniqueId    = uniqueId;
            this.MapPosition = mapPosition;
        }

        public string UniqueId { get; set; }

        public Vector3 MapPosition { get; set; }
    }
}
