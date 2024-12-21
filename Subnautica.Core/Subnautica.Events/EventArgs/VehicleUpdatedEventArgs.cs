namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class VehicleUpdatedEventArgs : EventArgs
    {
        public VehicleUpdatedEventArgs(string uniqueId, Vector3 position, Quaternion rotation, TechType techType, GameObject gameObject)
        {
            this.UniqueId = uniqueId;
            this.Position = position;
            this.Rotation = rotation;
            this.TechType = techType;
            this.Instance = gameObject;
        }

        public string UniqueId { get; private set; }

        public Vector3 Position { get; private set; }

        public Quaternion Rotation { get; private set; }

        public TechType TechType { get; private set; }

        public GameObject Instance { get; private set; }
    }
}
