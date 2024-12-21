namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class SpawnOnKillingEventArgs : EventArgs
    {
        public SpawnOnKillingEventArgs(string uniqueId, TechType techType, Vector3 position, Quaternion rotation, Vector3 velocity, ForceMode forceMode, bool isAllowed = true)
        {
            this.UniqueId  = uniqueId;
            this.TechType  = techType;
            this.Position  = position;
            this.Rotation  = rotation;
            this.Velocity  = velocity;
            this.ForceMode = forceMode;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public TechType TechType { get; set; }

        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public Vector3 Velocity { get; set; }

        public ForceMode ForceMode { get; set; }

        public bool IsAllowed { get; set; }
    }
}
