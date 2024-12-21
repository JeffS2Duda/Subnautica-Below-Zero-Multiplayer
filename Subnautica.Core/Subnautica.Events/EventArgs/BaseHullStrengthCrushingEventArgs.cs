namespace Subnautica.Events.EventArgs
{
    using System;

    public class BaseHullStrengthCrushingEventArgs : EventArgs
    {
        public BaseHullStrengthCrushingEventArgs(global::BaseHullStrength instance, bool isAllowed = true)
        {
            this.Instance = instance;
            this.IsAllowed = isAllowed;
        }

        public global::BaseHullStrength Instance { get; set; }

        public bool IsAllowed { get; set; }
    }
}
