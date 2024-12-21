namespace Subnautica.Network.Structures
{
    using System;
    using System.Collections.Generic;

    using MessagePack;

    [MessagePackObject]
    public class ZeroInt3 : IEquatable<ZeroInt3>
    {
        [Key(0)]
        public int X;

        [Key(1)]
        public int Y;

        [Key(2)]
        public int Z;

        public ZeroInt3()
        {
        }

        public ZeroInt3(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static bool operator ==(ZeroInt3 u, ZeroInt3 v)
        {
            if (u is null && v is null)
            {
                return true;
            }

            return u is ZeroInt3 && u.Equals(v);
        }

        public static bool operator !=(ZeroInt3 u, ZeroInt3 v)
        {
            return !(u == v);
        }

        public bool Equals(ZeroInt3 other)
        {
            if (other is null)
            {
                return false;
            }

            return other.X == this.X && other.Y == this.Y && other.Z == this.Z;
        }

        public override bool Equals(object obj)
        {
            return obj is ZeroInt3 other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 0;
                hash = (hash * 397) ^ this.X.GetHashCode();
                hash = (hash * 397) ^ this.Y.GetHashCode();
                hash = (hash * 397) ^ this.Z.GetHashCode();

                return hash;
            }
        }

        public IEnumerable<ZeroInt3> GetNeighbors(int max = 1)
        {
            for (int dx = -max; dx <= max; ++dx)
            {
                for (int dy = -max; dy <= max; ++dy)
                {
                    for (int dz = -max; dz <= max; ++dz)
                    {
                        yield return new ZeroInt3(this.X + dx, this.Y + dy, this.Z + dz);
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"[ZeroInt3: {this.X}, {this.Y}, {this.Z}]";
        }
    }
}
