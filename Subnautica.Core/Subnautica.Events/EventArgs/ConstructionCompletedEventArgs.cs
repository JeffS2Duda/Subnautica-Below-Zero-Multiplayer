namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class ConstructionCompletedEventArgs : EventArgs
    {
        public ConstructionCompletedEventArgs(string uniqueId, string baseId, TechType techType, Vector3 cellPosition, bool isFaceHasValue = false, Vector3 localPosition = new Vector3(), Quaternion localRotation = new Quaternion(), Base.Direction faceDirection = Base.Direction.North, Base.FaceType faceType = Base.FaceType.None)
        {
            this.UniqueId       = uniqueId;
            this.BaseId         = baseId;
            this.TechType       = techType;
            this.CellPosition   = cellPosition;
            this.IsFaceHasValue = isFaceHasValue;
            this.LocalPosition  = localPosition;
            this.LocalRotation  = localRotation;
            this.FaceDirection  = faceDirection;
            this.FaceType       = faceType;
        }

        public string UniqueId { get; private set; }

        public string BaseId { get; private set; }
        
        public TechType TechType { get; private set; }

        public Vector3 CellPosition { get; private set; }

        public bool IsFaceHasValue { get; private set; }

        public Vector3 LocalPosition { get; private set; }

        public Quaternion LocalRotation { get; private set; }

        public Base.Direction FaceDirection { get; private set; }

        public Base.FaceType FaceType { get; private set; }
    }
}
