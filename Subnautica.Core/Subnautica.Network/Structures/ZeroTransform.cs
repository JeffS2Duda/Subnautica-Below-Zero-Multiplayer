namespace Subnautica.Network.Structures
{
    using MessagePack;

    [MessagePackObject]
    public class ZeroTransform
    {
        [Key(0)]
        public ZeroVector3 Forward;

        [Key(1)]
        public ZeroVector3 Position;

        [Key(2)]
        public ZeroQuaternion Rotation;

        public ZeroTransform()
        {
        }

        public ZeroTransform(ZeroVector3 forward, ZeroVector3 position, ZeroQuaternion rotation)
        {
            this.Forward  = forward;
            this.Position = position;
            this.Rotation = rotation;
        }

        public ZeroTransform(ZeroVector3 position, ZeroQuaternion rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        public override string ToString()
        {
            return $"[ZeroTransform: {this.Forward}, {this.Position}, {this.Rotation}]";
        }
    }
}
