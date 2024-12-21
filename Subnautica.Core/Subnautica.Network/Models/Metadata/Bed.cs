namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;
    using Subnautica.Network.Core.Components;
    using System.Collections.Generic;

    [MessagePackObject]
    public class Bed : MetadataComponent
    {
        [Key(0)]
        public byte MaxPlayerCount { get; set; }

        [Key(1)]
        public bool IsSleeping { get; set; }

        [Key(2)]
        public BedSideItem CurrentSide { get; set; }

        [Key(3)]
        public List<BedSideItem> Sides { get; set; } = new List<BedSideItem>();

        public int GetBedEmptySideIndex()
        {
            if (this.MaxPlayerCount == 1)
            {
                return this.Sides.FindIndex(q => !q.IsUsing());
            }

            return this.Sides.FindIndex(q => !q.IsUsing() && q.Side == global::Bed.BedSide.None);
        }
    }

    [MessagePackObject]
    public class BedSideItem
    {
        [Key(0)]
        public string PlayerId { get; set; }

        [Key(1)]
        public global::Bed.BedSide Side { get; set; }

        [Key(2)]
        public float SleepTime { get; set; }

        [Key(3)]
        public byte PlayerId_v2 { get; set; }

        public BedSideItem()
        {

        }

        public BedSideItem(byte playerId, global::Bed.BedSide side)
        {
            this.PlayerId_v2 = playerId;
            this.PlayerId = null;
            this.Side = side;
        }

        public void Sleep(byte playerId, global::Bed.BedSide side, float SleepTime)
        {
            this.PlayerId = null;
            this.PlayerId_v2 = playerId;
            this.Side = side;
            this.SleepTime = SleepTime;
        }

        public void Standup()
        {
            this.PlayerId = null;
            this.PlayerId_v2 = 0;
            this.Side = global::Bed.BedSide.None;
            this.SleepTime = 0f;
        }

        public bool IsUsing()
        {
            return this.PlayerId_v2 > 0;
        }

        public bool IsSleeping(float currentTime)
        {
            return this.IsUsing() && currentTime >= this.SleepTime + 4f;
        }
    }
}
