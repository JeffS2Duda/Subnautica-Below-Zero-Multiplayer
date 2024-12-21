namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class BaseMoonpoolExpansionUndockTailEventArgs : EventArgs
    {
        public BaseMoonpoolExpansionUndockTailEventArgs(GameObject gameObject, bool withEjection, bool isAllowed = true)
        {
            this.GameObject   = gameObject;
            this.WithEjection = withEjection;
            this.IsAllowed    = isAllowed;
        }

        public GameObject GameObject { get; set; }

        public bool WithEjection { get; set; }

        public bool IsAllowed { get; set; }
    }
}
