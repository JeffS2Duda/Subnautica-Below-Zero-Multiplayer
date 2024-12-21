namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class ConstructionGhostTryPlacingEventArgs : EventArgs
    {
        public ConstructionGhostTryPlacingEventArgs(GameObject ghostModel,string uniqueId, string subrootId, TechType techType, int lastRotation, Vector3 position, Quaternion rotation, Transform aimTranform, bool isCanPlace, bool isBasePiece, bool isError, bool isAllowed = true)
        {
            this.GhostModel   = ghostModel;
            this.UniqueId     = uniqueId;
            this.SubrootId    = subrootId;
            this.TechType     = techType;
            this.LastRotation = lastRotation;
            this.Position     = position;
            this.Rotation     = rotation;
            this.AimTransform = aimTranform;
            this.IsCanPlace   = isCanPlace;
            this.IsBasePiece  = isBasePiece;
            this.IsError      = isError;
            this.IsAllowed    = isAllowed;
        }

        public GameObject GhostModel { get; private set; }

        public string UniqueId { get; private set; }

        public string SubrootId { get; private set; }

        public TechType TechType { get; private set; }

        public int LastRotation { get; private set; }

        public Vector3 Position { get; private set; }

        public Quaternion Rotation { get; private set; }

        public Transform AimTransform { get; private set; }

        public bool IsCanPlace { get; private set; }

        public bool IsBasePiece { get; private set; }

        public bool IsError { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
