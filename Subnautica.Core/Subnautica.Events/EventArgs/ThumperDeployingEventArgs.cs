namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class ThumperDeployingEventArgs : EventArgs
    {
        public ThumperDeployingEventArgs(string uniqueId, Pickupable pickupable, Vector3 deployPosition, float charge, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Pickupable = pickupable;
            this.DeployPosition = deployPosition;
            this.Charge = charge;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public Pickupable Pickupable { get; set; }

        public Vector3 DeployPosition { get; set; }

        public float Charge { get; set; }

        public bool IsAllowed { get; set; }
    }
}
