namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class LEDLightDeployingEventArgs : EventArgs
    {
        public LEDLightDeployingEventArgs(string uniqueId, Vector3 position, Quaternion rotation, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Position = position;
            this.Rotation = rotation;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public bool IsAllowed { get; set; }
    }
}
