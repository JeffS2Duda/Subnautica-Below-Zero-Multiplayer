namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class BaseMoonpoolExpansionDockingTimelineCompletingEventArgs : EventArgs
    {
        public BaseMoonpoolExpansionDockingTimelineCompletingEventArgs(GameObject gameObject, bool isAllowed = true)
        {
            this.GameObject = gameObject;
            this.IsAllowed = isAllowed;
        }

        public GameObject GameObject { get; set; }

        public bool IsAllowed { get; set; }
    }
}
