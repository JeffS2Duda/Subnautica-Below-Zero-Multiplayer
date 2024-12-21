namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class SpyPenguinDeployingEventArgs : EventArgs
    {
        public SpyPenguinDeployingEventArgs(string uniqueId, Pickupable pickupable, float health, string name, Vector3 position, Quaternion rotation, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Pickupable = pickupable;
            this.Name = name;
            this.Health = health;
            this.Position = position;
            this.Rotation = rotation;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public string Name { get; set; }

        public float Health { get; set; }

        public Pickupable Pickupable { get; set; }

        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public bool IsAllowed { get; set; }
    }
}
