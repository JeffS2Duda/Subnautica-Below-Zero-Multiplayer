namespace Subnautica.Network.Structures
{
    using MessagePack;

    [MessagePackObject]
    public class ZeroColor
    {
        [Key(0)]
        public float R { get; set; }

        [Key(1)]
        public float G { get; set; }

        [Key(2)]
        public float B { get; set; }

        [Key(3)]
        public float A { get; set; }

        public ZeroColor()
        {
        }

        public ZeroColor(float r, float g, float b, float a = 1)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public override string ToString()
        {
            return $"[ZeroColor: {this.R}, {this.G}, {this.B}, {this.A}]";
        }
    }
}
