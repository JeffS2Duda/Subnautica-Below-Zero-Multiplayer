namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class DroneCameraDeployingEventArgs : EventArgs
    {
        public DroneCameraDeployingEventArgs(string uniqueId, Pickupable pickupable, Vector3 deployPosition, Vector3 forward, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Pickupable = pickupable;
            this.DeployPosition = deployPosition;
            this.Forward = forward;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public Pickupable Pickupable { get; set; }

        public Vector3 DeployPosition { get; set; }

        public Vector3 Forward { get; set; }

        public bool IsAllowed { get; set; }
    }
}
