namespace Subnautica.API.Features.Creatures.Datas
{
    using System.Collections;
    using System.Collections.Generic;

    using Subnautica.API.Enums;
    using Subnautica.API.Features.Creatures.MonoBehaviours;
    using Subnautica.API.Features.Creatures.MonoBehaviours.Shared;
    using Subnautica.API.Features.Creatures.Trackers;

    using UnityEngine;

    using UWE;

    public abstract class BaseCreatureData
    {
        public abstract TechType CreatureType { get; set; }

        public abstract bool IsCanBeAttacked { get; set; }

        public abstract float Health { get; set; }

        public abstract float VisibilityDistance { get; set; }

        public abstract float VisibilityLongDistance { get; set; }

        public abstract bool IsRespawnable { get; set; }

        public abstract float StayAtLeashPositionWhenPassive { get; set; }

        public virtual float StayAtLeashPositionTime { get; set; } = 30000f;

        public virtual int RespawnTimeMin { get; set; }

        public virtual int RespawnTimeMax { get; set; }

        public virtual bool IsFastSyncActivated { get; set; }

        public virtual CreatureSpawnLevel SpawnLevel { get; set; } = CreatureSpawnLevel.Default;

        private byte CurrentAnimationIndex = 0;

        public delegate bool AnimationTrackerAction<T1, T2, T3, T4>(T1 a, T2 b, T3 c, out T4 d);

        private Dictionary<byte, BaseAnimationTracker> AnimationTrackers { get; set; } = new Dictionary<byte, BaseAnimationTracker>();

        public virtual void OnRegisterMonoBehaviours(MultiplayerCreature creature)
        {
            creature.GameObject.EnsureComponent<MultiplayerCreaturedShared>().SetMultiplayerCreature(creature);
        }

        public virtual IEnumerator OnCustomCreatureSpawnAsync(TaskResult<GameObject> task)
        {
            task.Set(null);
            yield return null;
        }

        public virtual GameObject OnCustomCreatureSpawn()
        {
            return null;
        }

        public virtual bool OnKill(GameObject gameObject)
        {
            return true;
        }

        public void AddAnimationTracker(BaseAnimationTracker tracker)
        {
            if (this.CurrentAnimationIndex < 250)
            {
                this.AnimationTrackers[++this.CurrentAnimationIndex] = tracker;
            }
            else
            {
                Log.Error($"Sooo much animation: {this.CurrentAnimationIndex}");
            }
        }
        public float GetVisibilityDistance(bool longDistance = false)
        {
            if (longDistance)
            {
                return this.VisibilityLongDistance * this.VisibilityLongDistance;
            }

            return this.VisibilityDistance * this.VisibilityDistance;
        }

        public int GetRespawnDuration()
        {
            if (this.RespawnTimeMin == this.RespawnTimeMax)
            {
                return this.RespawnTimeMin;
            }

            return Tools.Random.Next(this.RespawnTimeMin, this.RespawnTimeMax);
        }

        public bool HasAnimationTrackers()
        {
            return this.CurrentAnimationIndex > 0;
        }

        public BaseAnimationTracker GetAnimationTrackerById(byte animationId)
        {
            this.AnimationTrackers.TryGetValue(animationId, out var tracker);
            return tracker;
        }

        public Dictionary<byte, BaseAnimationTracker> GetAnimationTrackers()
        {
            return this.AnimationTrackers;
        }
    }
}
