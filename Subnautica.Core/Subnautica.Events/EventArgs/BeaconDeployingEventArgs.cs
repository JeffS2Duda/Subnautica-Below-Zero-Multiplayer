namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class BeaconDeployingEventArgs : EventArgs
    {
        public BeaconDeployingEventArgs(string uniqueId, Pickupable pickupable, Vector3 deployPosition, Quaternion deployRotation, bool isDeployedOnLand, string label, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Pickupable = pickupable;
            this.DeployPosition = deployPosition;
            this.DeployRotation = deployRotation;
            this.IsDeployedOnLand = isDeployedOnLand;
            this.Text = label;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public Pickupable Pickupable { get; set; }

        public Vector3 DeployPosition { get; set; }

        public Quaternion DeployRotation { get; set; }

        public bool IsDeployedOnLand { get; set; }

        public string Text { get; set; }

        public bool IsAllowed { get; set; }
    }
}
