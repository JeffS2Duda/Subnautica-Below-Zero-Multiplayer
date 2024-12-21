namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class ConstructorDeployingEventArgs : EventArgs
    {
        public ConstructorDeployingEventArgs(string uniqueId, Pickupable pickupable, Vector3 forward, Vector3 deployPosition, bool isAllowed = true)
        {
            this.UniqueId       = uniqueId;
            this.Pickupable     = pickupable;
            this.Forward        = forward;
            this.DeployPosition = deployPosition;
            this.IsAllowed      = isAllowed;
        }

        public string UniqueId { get; set; }

        public Pickupable Pickupable { get; set; }

        public Vector3 Forward { get; set; }

        public Vector3 DeployPosition { get; set; }

        public bool IsAllowed { get; set; }
    }
}
