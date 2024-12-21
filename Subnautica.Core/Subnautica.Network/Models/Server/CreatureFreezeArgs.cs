namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class CreatureFreezeArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.CreatureFreeze;

        [Key(5)]
        public ushort CreatureId { get; set; }

        [Key(6)]
        public float LifeTime { get; set; }

        [Key(7)]
        public string BrinicleId { get; set; }

        [Key(8)]
        public float EndTime { get; set; }

        public void UpdateEndTime(float currentTime)
        {
            if (!this.IsInfinityLifeTime())
            {
                this.EndTime = this.LifeTime + currentTime;
            }
        }

        public bool IsInfinityLifeTime()
        {
            return this.LifeTime == float.PositiveInfinity;
        }
    }
}
