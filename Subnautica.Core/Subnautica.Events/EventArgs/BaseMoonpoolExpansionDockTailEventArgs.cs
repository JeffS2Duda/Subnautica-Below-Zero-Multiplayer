namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class BaseMoonpoolExpansionDockTailEventArgs : EventArgs
    {
        public BaseMoonpoolExpansionDockTailEventArgs(GameObject gameObject, global::SeaTruckSegment newTail, bool isAllowed = true)
        {
            this.GameObject = gameObject;
            this.NewTail    = newTail;
            this.IsAllowed  = isAllowed;
        }

        public GameObject GameObject { get; set; }

        public global::SeaTruckSegment NewTail { get; set; }

        public bool IsAllowed { get; set; }
    }
}
