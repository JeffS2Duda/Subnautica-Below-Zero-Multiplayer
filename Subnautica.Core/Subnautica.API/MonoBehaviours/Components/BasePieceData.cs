namespace Subnautica.API.MonoBehaviours.Components
{
    using UnityEngine;

    public class BasePieceData
    {
        public Vector3 Position { get; set; }

        public Vector3 LocalPosition { get; set; }

        public Quaternion LocalRotation { get; set; }

        public Transform CurrentTransform { get; set; }

        public Base.Direction FaceDirection { get; set; }

        public Base.FaceType FaceType { get; set; }

        public TechType TechType { get; set; } = TechType.None;

        public override bool Equals(System.Object obj)
        {
            BasePieceData basePieceData = obj as BasePieceData;
            if (basePieceData is null)
            {
                return false;
            }

            return basePieceData.Position == this.Position && basePieceData.LocalPosition == this.LocalPosition && basePieceData.LocalRotation == this.LocalRotation && basePieceData.FaceDirection == this.FaceDirection && basePieceData.FaceType == this.FaceType && basePieceData.TechType == this.TechType && basePieceData.LocalRotation == this.LocalRotation;
        }

        public override int GetHashCode()
        {
            return (this.Position, this.LocalPosition, this.LocalRotation, this.FaceDirection, this.FaceType).GetHashCode();
        }
    }
}
