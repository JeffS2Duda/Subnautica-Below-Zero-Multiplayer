namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class VehicleUndockingEventArgs : EventArgs
    {
        public VehicleUndockingEventArgs(string uniqueId, string vehicleId, TechType MoonpoolType, Vector3 undockPosition, Quaternion undockRotation, bool isLeft, bool isAllowed = true)
        {
            this.UniqueId       = uniqueId;
            this.VehicleId      = vehicleId;
            this.MoonpoolType   = MoonpoolType;
            this.UndockPosition = undockPosition;
            this.UndockRotation = undockRotation;
            this.IsLeft         = isLeft;
            this.IsAllowed      = isAllowed;
        }

        public string UniqueId { get; set; }

        public string VehicleId { get; set; }

        public TechType MoonpoolType { get; set; }

        public Vector3 UndockPosition { get; set; }

        public Quaternion UndockRotation { get; set; }

        public bool IsLeft { get; set; }

        public bool IsAllowed { get; set; }
    }
}
