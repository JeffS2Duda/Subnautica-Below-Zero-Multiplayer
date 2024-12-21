namespace Subnautica.Network.Models.Storage.Story.Components
{
    using MessagePack;

    [MessagePackObject]
    public class GlacialBasinBridgeComponent
    {
        [Key(0)]
        public bool IsExtended { get; set; }

        [Key(1)]
        public bool IsFirstExtension { get; set; }

        [Key(2)]
        public float Time { get; set; }

        public bool Extend(float serverTime, float animationTime)
        {
            if (serverTime < this.Time)
            {
                return false;
            }

            this.IsExtended = true;
            this.Time = serverTime + animationTime;

            this.IsFirstExtension = true;
            return true;
        }

        public bool Retract(float serverTime, float animationTime)
        {
            if (serverTime < this.Time)
            {
                return false;
            }

            this.IsExtended = false;
            this.Time = serverTime + animationTime;
            return true;
        }
    }
}
