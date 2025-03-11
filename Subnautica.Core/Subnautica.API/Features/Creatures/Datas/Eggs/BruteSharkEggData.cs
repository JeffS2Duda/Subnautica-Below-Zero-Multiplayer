using Subnautica.API.Enums;
using Subnautica.API.Extensions;
using Subnautica.API.Features.Creatures.MonoBehaviours.Shared;
using System.Collections;
using UnityEngine;

namespace Subnautica.API.Features.Creatures.Datas.Eggs;

public class BruteSharkEggData : BaseCreatureData
{
    public override TechType CreatureType { get; set; } = TechType.BruteSharkEgg;

    public override bool IsCanBeAttacked { get; set; } = true;

    public override float Health { get; set; } = 200f;

    public override float VisibilityDistance { get; set; } = 30f;

    public override float VisibilityLongDistance { get; set; } = 40f;

    public override float StayAtLeashPositionTime { get; set; } = 10000f;

    public override float StayAtLeashPositionWhenPassive { get; set; } = 40f;

    public override bool IsRespawnable { get; set; } = false;

    public override CreatureSpawnLevel SpawnLevel { get; set; } = CreatureSpawnLevel.CustomAsync;

    public override IEnumerator OnCustomCreatureSpawnAsync(TaskResult<GameObject> task)
    {
        yield return CraftData.InstantiateFromPrefabAsync(this.CreatureType, task, false);
        yield return task.Get().BornAsync(task);
        yield break;
    }

    public override void OnRegisterMonoBehaviours(MultiplayerCreature creature)
    {
        base.OnRegisterMonoBehaviours(creature);
        Radical.EnsureComponent<MultiplayerWaterParkCreature>(creature.GameObject).SetMultiplayerCreature(creature);
    }
}