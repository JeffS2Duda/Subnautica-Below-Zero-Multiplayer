namespace Subnautica.API.Features.Creatures.Datas
{
    using Subnautica.API.Features.Creatures.MonoBehaviours.Shared;

    public class ShadowLeviathanData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.ShadowLeviathan;

        public override bool IsCanBeAttacked { get; set; } = true;

        public override float Health { get; set; } = 5000f;

        public override float VisibilityDistance { get; set; } = 200f;

        public override float VisibilityLongDistance { get; set; } = 250f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 100f;

        public override float StayAtLeashPositionTime { get; set; } = 15000f;

        public override bool IsRespawnable { get; set; } = false;

        public override bool IsFastSyncActivated { get; set; } = true;

        public override void OnRegisterMonoBehaviours(MultiplayerCreature creature)
        {
            base.OnRegisterMonoBehaviours(creature);

            creature.GameObject.EnsureComponent<MultiplayerMeleeAttack>().SetMultiplayerCreature(creature);
            creature.GameObject.EnsureComponent<MultiplayerAttackLastTarget>().SetMultiplayerCreature(creature);
            creature.GameObject.EnsureComponent<MultiplayerLeviathanMeleeAttack>().SetMultiplayerCreature(creature);
        }
    }
}
