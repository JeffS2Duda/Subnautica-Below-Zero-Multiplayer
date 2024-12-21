namespace Subnautica.Network.Structures
{
    using MessagePack;
    using System;

    [MessagePackObject]
    public class ZeroQuaternion : IEquatable<ZeroQuaternion>
    {
        [Key(0)]
        public float X;

        [Key(1)]
        public float Y;

        [Key(2)]
        public float Z;

        [Key(3)]
        public float W;

        public ZeroQuaternion()
        {
        }

        public ZeroQuaternion(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public static bool operator ==(ZeroQuaternion u, ZeroQuaternion v)
        {
            if (u is null && v is null)
            {
                return true;
            }

            return u is ZeroQuaternion && u.Equals(v);
        }

        public static bool operator !=(ZeroQuaternion u, ZeroQuaternion v)
        {
            return !(u == v);
        }

        public bool Equals(ZeroQuaternion other)
        {
            if (other is null)
            {
                return false;
            }

            return other.X == this.X && other.Y == this.Y && other.Z == this.Z && other.W == this.W;
        }

        public override bool Equals(object obj)
        {
            return obj is ZeroQuaternion other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 0;
                hash = (hash * 397) ^ this.X.GetHashCode();
                hash = (hash * 397) ^ this.Y.GetHashCode();
                hash = (hash * 397) ^ this.Z.GetHashCode();
                hash = (hash * 397) ^ this.W.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return $"[ZeroQuaternion: {this.X}, {this.Y}, {this.Z}, {this.W}]";
        }
    }
}
