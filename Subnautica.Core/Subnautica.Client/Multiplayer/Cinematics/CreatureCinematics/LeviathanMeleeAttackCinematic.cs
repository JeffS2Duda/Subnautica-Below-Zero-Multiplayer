namespace Subnautica.Client.Multiplayer.Cinematics.CreatureCinematics
{
    using FMODUnity;

    using Subnautica.API.Features;
    using Subnautica.Client.MonoBehaviours.Player;
    using Subnautica.Network.Structures;

    using UnityEngine;

    public class LeviathanMeleeAttackCinematic : CinematicController
    {
        private global::LeviathanMeleeAttack MeleeAttack { get; set; }

        public override void OnResetAnimations(PlayerCinematicQueueItem item)
        {
            this.MeleeAttack = this.Target.GetComponentInChildren<global::LeviathanMeleeAttack>();

            if (this.Target.TryGetComponent<global::AttackLastTarget>(out var attackLastTarget))
            {
                attackLastTarget.StopAttackSoundAndAnimation();
            }
        }

        public void StartMeleeAttack()
        {
            if (this.MeleeAttack)
            {
                this.MeleeAttack.timeLastBite = Time.time;

                this.SetCinematic(this.MeleeAttack.playerAttackCinematicController);
                this.StartCinematicMode();

                if (ZeroVector3.Distance(this.MeleeAttack.transform.position, global::Player.main.transform.position) < 3600f)
                {
                    FakeFMODByBenson.Instance.PlaySound(this.MeleeAttack.playerAttackSfx, this.Player.transform, 60f, this.IsValidSound);
                }
            }
        }

        private bool IsValidSound(StudioEventEmitter eventEmitter, Transform attachedTransform)
        {
            return this.IsCinematicModeActive && this.Player && attachedTransform;
        }
    }
}
