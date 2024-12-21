namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class ThermalLilyAnimationAnglesCheckingEventArgs : EventArgs
    {
        public ThermalLilyAnimationAnglesCheckingEventArgs(Vector3 position, float range, Vector3 playerPosition = default(Vector3), bool isAllowed = true)
        {
            this.LilyPosition   = position;
            this.PlayerRange    = range;
            this.PlayerPosition = playerPosition;
            this.IsAllowed      = isAllowed;
        }

        public Vector3 LilyPosition { get; private set; }

        public Vector3 PlayerPosition { get; set; }

        public float PlayerRange { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
