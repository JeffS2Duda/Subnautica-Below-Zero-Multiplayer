namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class BaseControlRoomMinimapMovingEventArgs : EventArgs
    {
        public BaseControlRoomMinimapMovingEventArgs(string uniqueId, Vector3 position)
        {
            this.UniqueId = uniqueId;
            this.Position = position;
        }

        public string UniqueId { get; set; }

        public Vector3 Position { get; set; }
    }
}
