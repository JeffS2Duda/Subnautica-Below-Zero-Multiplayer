namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class HoverpadUnDockingEventArgs : EventArgs
    {
        public HoverpadUnDockingEventArgs(string uniqueId, string itemId, Vector3 position, Quaternion rotation, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.ItemId = itemId;
            this.Position = position;
            this.Rotation = rotation;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public string ItemId { get; private set; }

        public Vector3 Position { get; private set; }

        public Quaternion Rotation { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
