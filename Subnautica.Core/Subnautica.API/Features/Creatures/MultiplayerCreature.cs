namespace Subnautica.API.Features.Creatures
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Models.Creatures;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class MultiplayerCreature
    {
        public bool IsActive { get; set; }

        public MultiplayerCreatureItem CreatureItem { get; set; }

        public MultiplayerCreatureMovement Movement { get; set; }

        public GameObject GameObject { get; set; }

        public global::Creature Creature { get; set; }

        public global::Locomotion Locomotion { get; set; }

        public Rigidbody Rigidbody { get; set; }

        private bool IsWaitingForRegistration { get; set; } = true;

        private static List<Type> ComponentTypes = new List<Type>()
        {
            typeof(global::Creature),
            typeof(global::Locomotion),
            typeof(global::WorldForces),
            typeof(global::SplineFollowing),

            typeof(global::FlyAwayWhenScared),
            typeof(global::MoveTowardsTarget),

            typeof(global::Drowning),
            typeof(global::Scareable),
            typeof(global::ScareableOfIceWorms),
            typeof(global::VentGardenSmall),
            typeof(global::CreaturePatrolling),
            typeof(global::SymbioteTitanHolefishInteraction),
            typeof(global::AggressiveWhenSeePlayer),
            typeof(global::AggressiveWhenSeeTarget),
            typeof(global::AggressiveToPilotingVehicle),
            typeof(global::CreatureDistantCall),
            typeof(global::CreatureEnterAreaCall),
            typeof(global::CreatureCallSound),
            typeof(global::LeviathanMeleeAttack),
            typeof(global::CreatureFollowPlayer),
            typeof(global::CreatureAggressionManager),
        };

        private HashSet<MonoBehaviour> Components = new HashSet<MonoBehaviour>();

        public MultiplayerCreature(GameObject gameObject)
        {
            this.GameObject = gameObject;
            this.Movement = new MultiplayerCreatureMovement(this);
            this.Rigidbody = this.GameObject.GetComponent<Rigidbody>();
            this.Creature = this.GameObject.GetComponent<global::Creature>();
            this.Locomotion = this.GameObject.GetComponent<global::Locomotion>();

            this.ComponentsToCache();
        }

        public void SetCreatureItem(MultiplayerCreatureItem creature)
        {
            this.CreatureItem = creature;
        }

        public void Spawn()
        {
            Log.Info("Spawn Creature ID: " + this.CreatureItem.Id);
            this.IsActive = true;

            if (this.GameObject)
            {
                this.ResetCreature();

                if (this.IsWaitingForRegistration)
                {
                    this.IsWaitingForRegistration = false;
                    this.CreatureItem.Data.OnRegisterMonoBehaviours(this);
                }

                this.GameObject.transform.position = this.CreatureItem.Position.ToVector3();
                this.GameObject.transform.rotation = this.CreatureItem.Rotation.ToQuaternion();
                this.GameObject.SetIdentityId(this.CreatureItem.Id.ToCreatureStringId());
                this.GameObject.SetActive(true);

                if (this.Creature && this.Creature.GetAnimator())
                {
                    this.Creature.GetAnimator().Rebind();
                    this.Creature.GetAnimator().enabled = true;
                }

                this.ToggleComponents(this.CreatureItem.IsMine());
                this.ToggleInterpolate(this.CreatureItem.IsMine());
                this.StartFirstAction(this.CreatureItem.IsMine());
            }
        }

        public void ChangeOwnership()
        {
            Log.Info("Ownership Changed Creature ID: " + this.CreatureItem.Id);
            this.ResetCreature();

            this.ToggleComponents(this.CreatureItem.IsMine());
            this.ToggleInterpolate(this.CreatureItem.IsMine());
            this.StartFirstAction(this.CreatureItem.IsMine());

            this.GameObject.SendMessage("OnChangedOwnership");
        }

        private void ToggleInterpolate(bool isMine)
        {
            if (this.Rigidbody)
            {
                if (isMine)
                {
                    this.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                    this.Rigidbody.SetNonKinematic();
                }
                else
                {
                    this.Rigidbody.interpolation = RigidbodyInterpolation.None;
                    this.Rigidbody.SetKinematic();
                }
            }
        }

        private void StartFirstAction(bool isMine)
        {
            if (this.Creature && isMine)
            {
                if (this.GameObject.TryGetComponent<global::StayAtLeashPosition>(out var tmpAction) && this.CreatureItem.Position.Distance(this.GameObject.transform.position) >= tmpAction.leashDistance * tmpAction.leashDistance)
                {
                    this.Creature.TryStartAction(tmpAction);
                }
                else
                {
                    this.Creature.UpdateBehaviour(Time.time, Time.deltaTime);
                }
            }
        }

        public void Disable()
        {
            Log.Info("Disable Creature ID: " + this.CreatureItem.Id);

            this.Movement.ResetCreature();

            if (this.GameObject)
            {
                this.GameObject.SetActive(false);
            }

            this.IsActive = false;
            this.CreatureItem = null;
        }

        private void ResetCreature()
        {
            this.Movement.ResetCreature();

            if (this.Creature)
            {
                this.Creature.leashPosition = this.CreatureItem.LeashPosition.ToVector3();
                this.Creature.lastUpdateTime = -1f;
                this.Creature.indexLastActionChecked = 0;

                if (this.Creature.prevBestAction)
                {
                    this.Creature.prevBestAction.StopPerform(Time.time);
                    this.Creature.prevBestAction = null;
                }

                if (this.Creature.lastAction)
                {
                    this.Creature.lastAction.StopPerform(Time.time);
                    this.Creature.lastAction = null;
                }

                foreach (var action in this.Creature.actions)
                {
                    action.StopPerform(Time.time);
                    action.timeLastChecked = 0;
                }

                if (this.Creature.initialCuriosity?.length > 0)
                {
                    this.Creature.Curiosity.Value = this.Creature.initialCuriosity.Evaluate(UnityEngine.Random.value);
                }

                if (this.Creature.initialFriendliness?.length > 0)
                {
                    this.Creature.Friendliness.Value = this.Creature.initialFriendliness.Evaluate(UnityEngine.Random.value);
                }

                if (this.Creature.initialHunger?.length > 0)
                {
                    this.Creature.Hunger.Value = this.Creature.initialHunger.Evaluate(UnityEngine.Random.value);
                }

                this.Creature.Tired.Value = 1f - DayNightUtils.Evaluate(1f, this.Creature.activity);
                this.Creature.Scared.Value = 0f;
                this.Creature.Aggression.Value = 0f;
                this.Creature.leashPosition = this.CreatureItem.LeashPosition.ToVector3();

                if (this.Creature.GetAnimator())
                {
                    this.Creature.GetAnimator().SetFloat(Creature.animAggressive, this.Creature.Aggression.Value);
                    this.Creature.GetAnimator().SetFloat(Creature.animScared, this.Creature.Scared.Value);
                    this.Creature.GetAnimator().SetFloat(Creature.animTired, this.Creature.Tired.Value);
                    this.Creature.GetAnimator().SetFloat(Creature.animHappy, this.Creature.Happy.Value);
                }
            }
        }

        private void ComponentsToCache()
        {
            if (this.GameObject)
            {
                foreach (var item in ComponentTypes)
                {
                    foreach (var children in this.GameObject.GetComponentsInChildren(item))
                    {
                        this.Components.Add(children as MonoBehaviour);
                    }
                }
            }
        }


        private void ToggleComponents(bool isEnable)
        {
            foreach (var item in this.Components)
            {
                if (item.enabled != isEnable)
                {
                    item.enabled = isEnable;
                }
            }
        }

        public void OnKill(MultiplayerCreature creature)
        {
            if (creature != null && creature.GameObject.TryGetComponent<global::LiveMixin>(out var liveMixin))
            {
                this.GameObject?.SendMessage("OnMultiplayerKill");

                creature.GameObject.transform.position = this.GameObject.transform.position;
                creature.GameObject.transform.rotation = this.GameObject.transform.rotation;
                creature.GameObject.SetActive(true);

                if (this.CreatureItem.Data.OnKill(creature.GameObject))
                {
                    ZeroLiveMixin.TakeDamage(liveMixin, 0f);

                    if (liveMixin && liveMixin.gameObject)
                    {
                        GameObject.Destroy(liveMixin.gameObject, 60f);
                    }
                }
            }
        }
    }
}
