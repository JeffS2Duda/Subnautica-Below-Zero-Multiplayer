namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class ThermalLilyRangeCheckingEventArgs : EventArgs
    {
        public ThermalLilyRangeCheckingEventArgs(Vector3 position, float range, bool isPlayerInRange = false, bool isAllowed = true)
        {
            this.LilyPosition = position;
            this.PlayerRange = range;
            this.IsPlayerInRange = isPlayerInRange;
            this.IsAllowed = isAllowed;
        }

        public Vector3 LilyPosition { get; private set; }

        public float PlayerRange { get; set; }

        public bool IsPlayerInRange { get; set; }

        public bool IsAllowed { get; set; }
    }
}
