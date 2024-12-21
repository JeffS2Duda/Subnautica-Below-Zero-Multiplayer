namespace Subnautica.API.Features.Creatures.Datas
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features.Creatures.MonoBehaviours.Shared;

    using UnityEngine;

    public class VoidLeviathanData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.GhostLeviathan;

        public override bool IsCanBeAttacked { get; set; } = true;

        public override float Health { get; set; } = 5000f;

        public override float VisibilityDistance { get; set; } = 220f;

        public override float VisibilityLongDistance { get; set; } = 250f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 150f;

        public override float StayAtLeashPositionTime { get; set; } = 10000f;

        public override bool IsRespawnable { get; set; } = false;

        public override bool IsFastSyncActivated { get; set; } = true;

        public override CreatureSpawnLevel SpawnLevel { get; set; } = CreatureSpawnLevel.Custom;

        public override GameObject OnCustomCreatureSpawn()
        {
            var gameObject = UnityEngine.Object.Instantiate<GameObject>(VoidLeviathansSpawner.main.ghostLeviathanPrefab, Vector3.zero, Quaternion.identity, false);
            gameObject.SetTechType(TechType.GhostLeviathan);
            gameObject.SetIdentityId(Network.Identifier.GenerateUniqueId());
            return gameObject;
        }

        public override void OnRegisterMonoBehaviours(MultiplayerCreature creature)
        {
            base.OnRegisterMonoBehaviours(creature);

            creature.GameObject.EnsureComponent<MultiplayerMeleeAttack>().SetMultiplayerCreature(creature);
            creature.GameObject.EnsureComponent<MultiplayerAttackLastTarget>().SetMultiplayerCreature(creature);
        }
    }
}
