namespace Subnautica.API.Features.Creatures.Datas
{
    using Subnautica.API.Features.Creatures.MonoBehaviours.Shared;

    public class BruteSharkData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.BruteShark;

        public override bool IsCanBeAttacked { get; set; } = true;

        public override float Health { get; set; } = 200f;

        public override float VisibilityDistance { get; set; } = 90f;

        public override float VisibilityLongDistance { get; set; } = 115f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 50f;

        public override float StayAtLeashPositionTime { get; set; } = 15000f;

        public override bool IsRespawnable { get; set; } = true;

        public override int RespawnTimeMin { get; set; } = 600;

        public override int RespawnTimeMax { get; set; } = 600;

        public override void OnRegisterMonoBehaviours(MultiplayerCreature creature)
        {
            base.OnRegisterMonoBehaviours(creature);

            creature.GameObject.EnsureComponent<MultiplayerMeleeAttack>().SetMultiplayerCreature(creature);
            creature.GameObject.EnsureComponent<MultiplayerAttackLastTarget>().SetMultiplayerCreature(creature);
            creature.GameObject.EnsureComponent<MultiplayerCreatureAggressionManager>().SetMultiplayerCreature(creature);
        }
    }
}
