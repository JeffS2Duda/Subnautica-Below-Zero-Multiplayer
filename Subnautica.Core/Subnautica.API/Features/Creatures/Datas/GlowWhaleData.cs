namespace Subnautica.API.Features.Creatures.Datas
{
    using Subnautica.API.Features.Creatures.MonoBehaviours;

    public class GlowWhaleData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.GlowWhale;

        public override bool IsCanBeAttacked { get; set; } = true;

        public override float Health { get; set; } = 10000f;

        public override float VisibilityDistance { get; set; } = 160f;

        public override float VisibilityLongDistance { get; set; } = 200f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 120f;

        public override bool IsRespawnable { get; set; } = true;

        public override int RespawnTimeMin { get; set; } = 1200;

        public override int RespawnTimeMax { get; set; } = 1200;

        public override void OnRegisterMonoBehaviours(MultiplayerCreature creature)
        {
            base.OnRegisterMonoBehaviours(creature);
            
            creature.GameObject.EnsureComponent<GlowWhaleMonoBehaviour>().SetMultiplayerCreature(creature);
        }
    }
}
