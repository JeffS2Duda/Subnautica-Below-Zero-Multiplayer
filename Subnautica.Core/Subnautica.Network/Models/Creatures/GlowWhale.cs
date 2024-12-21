namespace Subnautica.Network.Models.Creatures
{
    using MessagePack;

    using Subnautica.API.Enums.Creatures;
    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class GlowWhale : NetworkCreatureComponent
    {
        [Key(0)]
        public bool IsRideStart { get; set; }

        [Key(1)]
        public bool IsRideEnd { get; set; }

        [Key(2)]
        public bool IsEyeInteract { get; set; }

        [Key(3)]
        public GlowWhaleSFXType SFXType { get; set; }

        public GlowWhale()
        {

        }

        public GlowWhale(bool isRideStart, bool isRideEnd, bool isEyeInteract, GlowWhaleSFXType sfxType)
        {
            this.IsRideStart   = isRideStart;
            this.IsRideEnd     = isRideEnd;
            this.IsEyeInteract = isEyeInteract;
            this.SFXType       = sfxType;
        }
    }
}
