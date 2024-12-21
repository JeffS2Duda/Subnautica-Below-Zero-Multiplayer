namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class BaseMoonpoolExpansionUndockingTimelineCompletingEventArgs : EventArgs
    {
        public BaseMoonpoolExpansionUndockingTimelineCompletingEventArgs(GameObject gameObject, bool isAllowed = true)
        {
            this.GameObject = gameObject;
            this.IsAllowed = isAllowed;
        }

        public GameObject GameObject { get; set; }

        public bool IsAllowed { get; set; }
    }
}
