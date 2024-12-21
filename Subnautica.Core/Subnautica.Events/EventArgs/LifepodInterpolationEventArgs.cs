namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class LifepodInterpolationEventArgs : EventArgs
    {
        public LifepodInterpolationEventArgs(GameObject dropObject, bool isAllowed = true)
        {
            this.DropObject  = dropObject;
            this.IsAllowed   = isAllowed;
        }

        public GameObject DropObject { get; set; }

        public bool IsCompleted { get; set; }

        public Quaternion Rotation { get; set; }

        public float StartedTime { get; set; }

        public bool IsAllowed { get; set; }
    }
}
