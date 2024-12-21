namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class FlareDeployingEventArgs : EventArgs
    {
        public FlareDeployingEventArgs(string uniqueId, Pickupable pickupable, Vector3 deployPosition, float energy, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Pickupable = pickupable;
            this.DeployPosition = deployPosition;
            this.Forward = global::MainCamera.camera.transform.forward;
            this.Energy = energy;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public Pickupable Pickupable { get; private set; }

        public Vector3 DeployPosition { get; private set; }

        public Vector3 Forward { get; private set; }

        public float Energy { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
