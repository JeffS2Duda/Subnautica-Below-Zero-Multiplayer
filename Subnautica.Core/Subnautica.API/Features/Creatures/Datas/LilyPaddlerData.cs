namespace Subnautica.API.Features.Creatures.Datas
{
    using Subnautica.API.Features.Creatures.MonoBehaviours;
    using Subnautica.API.Features.Creatures.Trackers;

    public class LilyPaddlerData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.LilyPaddler;

        public override bool IsCanBeAttacked { get; set; } = true;

        public override float Health { get; set; } = 100f;

        public override float VisibilityDistance { get; set; } = 100f;

        public override float VisibilityLongDistance { get; set; } = 125f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 75f;

        public override bool IsRespawnable { get; set; } = true;

        public override int RespawnTimeMin { get; set; } = 420;

        public override int RespawnTimeMax { get; set; } = 420;

        public LilyPaddlerData()
        {
            this.AddAnimationTracker(new CreatureAttackTracker());
        }

        public override void OnRegisterMonoBehaviours(MultiplayerCreature creature)
        {
            base.OnRegisterMonoBehaviours(creature);
            
            creature.GameObject.EnsureComponent<LilyPaddlerMonoBehaviour>().SetMultiplayerCreature(creature);
        }
    }
}
