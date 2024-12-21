namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class PlayerItemDropingEventArgs : EventArgs
    {
        public PlayerItemDropingEventArgs(string uniqueId, Pickupable item, Vector3 position, Quaternion rotation,  bool isAllowed = true)
        {
            this.UniqueId  = uniqueId;
            this.Item      = item;
            this.Position  = position;
            this.Rotation  = rotation;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public Pickupable Item { get; private set; }

        public Vector3 Position { get; private set; }

        public Quaternion Rotation { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
