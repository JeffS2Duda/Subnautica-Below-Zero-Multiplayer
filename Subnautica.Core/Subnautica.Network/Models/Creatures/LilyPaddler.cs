namespace Subnautica.Network.Models.Creatures
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class LilyPaddler : NetworkCreatureComponent
    {
        [Key(0)]
        public byte TargetId { get; set; }

        [Key(1)]
        public float LastHypnotizeTime { get; set; }

        public LilyPaddler()
        {

        }

        public LilyPaddler(byte targetId)
        {
            this.TargetId = targetId;
        }
    }
}
