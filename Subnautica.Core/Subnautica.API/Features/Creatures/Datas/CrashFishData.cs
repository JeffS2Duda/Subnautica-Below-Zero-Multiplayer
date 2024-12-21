namespace Subnautica.API.Features.Creatures.Datas
{
    using Subnautica.API.Features.Creatures.MonoBehaviours;
    using Subnautica.API.Features.Creatures.Trackers;

    using UnityEngine;

    public class CrashFishData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.Crash;

        public override bool IsCanBeAttacked { get; set; } = false;

        public override float Health { get; set; } = 25f;

        public override float VisibilityDistance { get; set; } = 50f;

        public override float VisibilityLongDistance { get; set; } = 80f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 0f;

        public override float StayAtLeashPositionTime { get; set; } = 0f;

        public override bool IsRespawnable { get; set; } = true;

        public override int RespawnTimeMin { get; set; } = 900;

        public override int RespawnTimeMax { get; set; } = 1200;

        public CrashFishData()
        {
            this.AddAnimationTracker(new ProtectCrashHomeTracker());
        }

        public override void OnRegisterMonoBehaviours(MultiplayerCreature creature)
        {
            base.OnRegisterMonoBehaviours(creature);

            creature.GameObject.EnsureComponent<CrashFishMonobehaviour>().SetMultiplayerCreature(creature);
        }

        public override bool OnKill(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<global::Crash>(out var crash))
            {
                SafeAnimator.SetBool(crash.GetAnimator(), "explode", true);

                if (crash.detonateParticlePrefab)
                {
                    Utils.PlayOneShotPS(crash.detonateParticlePrefab, crash.transform.position, crash.transform.rotation);
                }

                DamageSystem.RadiusDamage(crash.maxDamage, crash.transform.position, crash.detonateRadius, DamageType.Explosive, crash.gameObject);
            }

            gameObject.GetComponent<LiveMixin>().Kill();
            return false;
        }
    }
}
