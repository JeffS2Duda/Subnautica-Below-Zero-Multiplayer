namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class HoverbikeDeployingEventArgs : EventArgs
    {
        public HoverbikeDeployingEventArgs(string uniqueId, Hoverbike hoverbike, Vector3 deployPosition, Vector3 forward, bool isAllowed = true)
        {
            this.UniqueId       = uniqueId;
            this.Hoverbike      = hoverbike;
            this.DeployPosition = deployPosition;
            this.Forward        = forward;
            this.IsAllowed      = isAllowed;
        }

        public string UniqueId { get; set; }

        public Hoverbike Hoverbike { get; set; }

        public Vector3 DeployPosition { get; set; }

        public Vector3 Forward { get; set; }

        public bool IsAllowed { get; set; }
    }
}
