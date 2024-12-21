namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class PipeSurfaceFloaterDeployingEventArgs : EventArgs
    {
        public PipeSurfaceFloaterDeployingEventArgs(string uniqueId, Pickupable pickupable, Vector3 deployPosition, Quaternion deployRotation, bool isAllowed = true)
        {
            this.UniqueId       = uniqueId;
            this.Pickupable     = pickupable;
            this.DeployPosition = deployPosition;
            this.DeployRotation = deployRotation;
            this.IsAllowed      = isAllowed;
        }

        public string UniqueId { get; set; }

        public Pickupable Pickupable { get; set; }

        public Vector3 DeployPosition { get; set; }

        public Quaternion DeployRotation { get; set; }

        public bool IsAllowed { get; set; }
    }
}
