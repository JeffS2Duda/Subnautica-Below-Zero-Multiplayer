namespace Subnautica.Events.EventArgs
{
    using System;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;

    using UnityEngine;

    public class ConstructionGhostMovedEventArgs : EventArgs
    {
        public ConstructionGhostMovedEventArgs(GameObject ghostModel, TechType techType, Transform aimTranform, bool isCanPlace, int lastRotation)
        {
            this.GhostModel      = ghostModel;
            this.UniqueId        = ghostModel.GetIdentityId(true);
            this.TechType        = techType;
            this.Position        = ghostModel.transform.position;
            this.Rotation        = ghostModel.transform.rotation;
            this.AimTransform    = aimTranform;
            this.IsCanPlace      = isCanPlace;
            this.UpdatePlacement = Network.Temporary.GetProperty<bool>(ghostModel.GetIdentityId(), "UpdatePlacementResult");
            this.LastRotation    = lastRotation;
        }

        public GameObject GhostModel { get; private set; }

        public TechType TechType { get; private set; }

        public Vector3 Position { get; private set; }

        public Quaternion Rotation { get; private set; }

        public Transform AimTransform { get; private set; }

        public string UniqueId { get; private set; }

        public bool IsCanPlace { get; private set; }

        public bool UpdatePlacement { get; private set; }

        public int LastRotation { get; private set; }
    }
}
