namespace Subnautica.API.Features.Creatures.MonoBehaviours.Shared
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;

    using UnityEngine;

    public class MultiplayerAttackLastTarget : BaseMultiplayerCreature
    {
        public global::AttackLastTarget AttackLastTarget { get; private set; }

        public void Awake()
        {
            this.AttackLastTarget = this.GetComponent<global::AttackLastTarget>();
        }

        public void ForceAttackTarget(GameObject target)
        {
            if (this.MultiplayerCreature.CreatureItem.IsMine())
            {
                using (EventBlocker.Create(ProcessType.CreatureAttackLastTarget))
                {
                    this.AttackLastTarget.creature.StopPrevAction();

                    if (target)
                    {
                        this.AttackLastTarget.ForceAttackTarget(target);
                    }
                }
            }
            else 
            {
                this.AttackLastTarget.StartAttackSoundAndAnimation();
            }
        }

        public void OnChangedOwnership()
        {
            this.StopAttackSoundAndAnimation();
        }

        public void OnDisable()
        {
            this.StopAttackSoundAndAnimation();
        }

        public void StopAttackSoundAndAnimation(bool onlyAnimationAndSounds = false)
        {
            if (this.AttackLastTarget)
            {
                this.AttackLastTarget.StopAttackSoundAndAnimation();

                if (!onlyAnimationAndSounds)
                {
                    this.AttackLastTarget.StopAttack();
                }
            }
        }
    }
}
