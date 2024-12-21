namespace Subnautica.API.Features.Creatures.Datas
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features.Creatures.MonoBehaviours.Shared;
    using System.Collections;
    using UnityEngine;

    public class GlowWhaleEggData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.GlowWhaleEgg;

        public override bool IsCanBeAttacked { get; set; } = true;

        public override float Health { get; set; } = 100f;

        public override float VisibilityDistance { get; set; } = 60f;

        public override float VisibilityLongDistance { get; set; } = 80f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 40f;

        public override bool IsRespawnable { get; set; } = false;

        public override CreatureSpawnLevel SpawnLevel { get; set; } = CreatureSpawnLevel.CustomAsync;

        public override IEnumerator OnCustomCreatureSpawnAsync(TaskResult<GameObject> task)
        {
            yield return CraftData.InstantiateFromPrefabAsync(this.CreatureType, task);
            yield return task.Get().BornAsync(task);
        }

        public override void OnRegisterMonoBehaviours(MultiplayerCreature creature)
        {
            base.OnRegisterMonoBehaviours(creature);

            creature.GameObject.EnsureComponent<MultiplayerWaterParkCreature>().SetMultiplayerCreature(creature);
        }
    }
}