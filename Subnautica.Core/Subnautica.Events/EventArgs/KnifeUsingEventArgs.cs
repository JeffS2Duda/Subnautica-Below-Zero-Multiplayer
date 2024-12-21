namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class KnifeUsingEventArgs : EventArgs
    {
        public KnifeUsingEventArgs(VFXEventTypes vFXEventType, Vector3 targetPosition, Vector3 orientation, VFXSurfaceTypes surfaceType, VFXSurfaceTypes soundSurfaceType, bool isUnderwater)
        {
            this.VFXEventType = vFXEventType;
            this.TargetPosition = targetPosition;
            this.Orientation = orientation;
            this.SurfaceType = surfaceType;
            this.SoundSurfaceType = soundSurfaceType;
            this.IsUnderwater = isUnderwater;
        }

        public VFXEventTypes VFXEventType { get; set; }

        public Vector3 TargetPosition { get; set; }

        public Vector3 Orientation { get; set; }

        public VFXSurfaceTypes SurfaceType { get; set; }

        public VFXSurfaceTypes SoundSurfaceType { get; set; }

        public bool IsUnderwater { get; set; }
    }
}
