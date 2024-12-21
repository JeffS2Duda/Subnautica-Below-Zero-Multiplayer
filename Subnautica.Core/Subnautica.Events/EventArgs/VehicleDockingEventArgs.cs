namespace Subnautica.Events.EventArgs
{
    using System;

    using Subnautica.API.Features;

    using UnityEngine;

    public class VehicleDockingEventArgs : EventArgs
    {
        public VehicleDockingEventArgs(string uniqueId, GameObject vehicle, TechType MoonpoolType, Vector3 backModulePosition, Vector3 endPosition, Quaternion endRotation, bool isAllowed = true)
        {
            this.UniqueId     = uniqueId;
            this.VehicleId    = Network.Identifier.GetIdentityId(vehicle, false);
            this.Vehicle      = vehicle;
            this.MoonpoolType = MoonpoolType;
            this.EndPosition  = endPosition;
            this.EndRotation  = endRotation;
            this.BackModulePosition = backModulePosition;
            this.IsAllowed    = isAllowed;
        }

        public string UniqueId { get; set; }

        public string VehicleId { get; set; }

        public GameObject Vehicle { get; set; }

        public TechType MoonpoolType { get; set; }

        public Vector3 BackModulePosition { get; set; }

        public Vector3 EndPosition { get; set; }

        public Quaternion EndRotation { get; set; }

        public bool IsAllowed { get; set; }
    }
}
