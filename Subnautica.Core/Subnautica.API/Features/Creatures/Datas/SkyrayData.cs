namespace Subnautica.API.Features.Creatures.Datas
{
    using Subnautica.API.Features.Creatures.MonoBehaviours;
    using Subnautica.API.Features.Creatures.Trackers;

    public class SkyrayData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.Skyray;

        public override bool IsCanBeAttacked { get; set; } = true;

        public override float Health { get; set; } = 100f;

        public override float VisibilityDistance { get; set; } = 100f;

        public override float VisibilityLongDistance { get; set; } = 120f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 80f;

        public override bool IsRespawnable { get; set; } = true;

        public override int RespawnTimeMin { get; set; } = 300;

        public override int RespawnTimeMax { get; set; } = 300;

        public SkyrayData()
        {
            this.AddAnimationTracker(new FlappingAnimationTracker());
        }

        public override void OnRegisterMonoBehaviours(MultiplayerCreature creature)
        {
            base.OnRegisterMonoBehaviours(creature);

            creature.GameObject.EnsureComponent<SkyrayMonoBehaviour>().SetMultiplayerCreature(creature);
        }
    }
}
