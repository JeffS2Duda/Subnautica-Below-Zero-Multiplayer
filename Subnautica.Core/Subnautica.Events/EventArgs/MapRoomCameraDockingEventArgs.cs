namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class MapRoomCameraDockingEventArgs : EventArgs
    {
        public MapRoomCameraDockingEventArgs(string uniqueId, string vehicleId, Vector3 endPosition, Quaternion endRotation, bool isLeft, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.VehicleId = vehicleId;
            this.EndPosition = endPosition;
            this.EndRotation = endRotation;
            this.IsLeft = isLeft;
        }

        public string UniqueId { get; set; }

        public string VehicleId { get; set; }

        public Vector3 EndPosition { get; set; }

        public Quaternion EndRotation { get; set; }

        public bool IsLeft { get; set; }

        public bool IsAllowed { get; set; }
    }
}
