namespace Subnautica.Events.EventArgs
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;

    public class PlayerUpdatedEventArgs : EventArgs
    {
        public PlayerUpdatedEventArgs(Vector3 position, Vector3 localPosition, Quaternion rotation, TechType techTypeInHand, List<TechType> equipments, float cameraPitch, Vector3 cameraForward, float emoteIndex, bool isPrecursorArm, bool isInWaterPark, VFXSurfaceTypes surfaceType)
        {
            this.Position = position;
            this.LocalPosition = localPosition;
            this.Rotation = rotation;
            this.TechTypeInHand = techTypeInHand;
            this.Equipments = equipments;
            this.CameraPitch = cameraPitch;
            this.CameraForward = cameraForward;
            this.EmoteIndex = emoteIndex;
            this.IsPrecursorArm = isPrecursorArm;
            this.IsInWaterPark = isInWaterPark;
            this.SurfaceType = surfaceType;
        }

        public Vector3 Position { get; private set; }

        public Vector3 LocalPosition { get; private set; }

        public Quaternion Rotation { get; private set; }

        public TechType TechTypeInHand { get; private set; }

        public List<TechType> Equipments { get; private set; }

        public float CameraPitch { get; private set; }

        public Vector3 CameraForward { get; private set; }

        public float EmoteIndex { get; private set; }

        public bool IsPrecursorArm { get; private set; }

        public bool IsInWaterPark { get; private set; }

        public VFXSurfaceTypes SurfaceType { get; private set; }
    }
}
