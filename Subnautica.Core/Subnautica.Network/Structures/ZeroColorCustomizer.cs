namespace Subnautica.Network.Structures
{
    using MessagePack;

    [MessagePackObject]
    public class ZeroColorCustomizer
    {
        [Key(0)]
        public string Name { get; set; }

        [Key(1)]
        public ZeroColor BaseColor { get; set; }

        [Key(2)]
        public ZeroColor StripeColor1 { get; set; }

        [Key(3)]
        public ZeroColor StripeColor2 { get; set; }

        [Key(4)]
        public ZeroColor NameColor { get; set; }

        public ZeroColorCustomizer()
        {
        }

        public ZeroColorCustomizer(string name, ZeroColor baseColor, ZeroColor stripColor1, ZeroColor stripColor2, ZeroColor nameColor)
        {
            this.Name = name;
            this.BaseColor = baseColor;
            this.StripeColor1 = stripColor1;
            this.StripeColor2 = stripColor2;
            this.NameColor = nameColor;
        }

        public void CopyFrom(ZeroColorCustomizer colorCustomizer)
        {
            this.Name = colorCustomizer.Name;
            this.BaseColor = colorCustomizer.BaseColor;
            this.StripeColor1 = colorCustomizer.StripeColor1;
            this.StripeColor2 = colorCustomizer.StripeColor2;
            this.NameColor = colorCustomizer.NameColor;
        }
    }
}
