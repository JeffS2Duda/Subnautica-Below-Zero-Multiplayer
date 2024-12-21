namespace Subnautica.API.Features.Creatures.Datas
{
    using Subnautica.API.Features.Creatures.MonoBehaviours;

    public class JellyFishData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.Jellyfish;

        public override bool IsCanBeAttacked { get; set; } = true;

        public override float Health { get; set; } = 200f;

        public override float VisibilityDistance { get; set; } = 100f;

        public override float VisibilityLongDistance { get; set; } = 125f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 80f;

        public override bool IsRespawnable { get; set; } = true;

        public override int RespawnTimeMin { get; set; } = 600;

        public override int RespawnTimeMax { get; set; } = 600;

        public override void OnRegisterMonoBehaviours(MultiplayerCreature creature)
        {
            base.OnRegisterMonoBehaviours(creature);

            creature.GameObject.EnsureComponent<JellyFishMonobehaviour>().SetMultiplayerCreature(creature);
        }
    }
}
