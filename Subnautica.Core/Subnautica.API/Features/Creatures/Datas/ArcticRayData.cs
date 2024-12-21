namespace Subnautica.API.Features.Creatures.Datas
{
    using Subnautica.API.Features.Creatures.Trackers;

    public class ArcticRayData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.ArcticRay;

        public override bool IsCanBeAttacked { get; set; } = true;

        public override float Health { get; set; } = 100f;

        public override float VisibilityDistance { get; set; } = 75f;

        public override float VisibilityLongDistance { get; set; } = 95f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 60f;

        public override bool IsRespawnable { get; set; } = true;

        public override int RespawnTimeMin { get; set; } = 420;

        public override int RespawnTimeMax { get; set; } = 420;

        public ArcticRayData()
        {
            this.AddAnimationTracker(new ArcticRayAnimationTracker());
        }
    }
}
