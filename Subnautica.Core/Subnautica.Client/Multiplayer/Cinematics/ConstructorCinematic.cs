namespace Subnautica.Client.Multiplayer.Cinematics
{
    using Subnautica.Client.MonoBehaviours.Player;

    using UnityEngine;

    public class ConstructorCinematic : CinematicController
    {
        private global::ConstructorCinematicController Constructor { get; set; }

        public override void OnResetAnimations(PlayerCinematicQueueItem item)
        {
            this.Constructor = this.Target.GetComponentInChildren<global::ConstructorCinematicController>();
            this.Constructor.animator.Play(AnimatorHashID.deployed, 0, 1f);
            this.Constructor.animator.SetBool(AnimatorHashID.deployed, true);
        }

        public void EngageStartCinematic()
        {
            this.Constructor.ResetAnimParams(this.PlayerAnimator);

            this.SetCinematic(this.Constructor.engageCinematicController);
            this.StartCinematicMode();
        }

        public void DisengageStartCinematic()
        {
            this.EngageConditionSimulate();

            this.Constructor.ResetAnimParams(this.PlayerAnimator);

            this.SetCinematic(this.Constructor.disengageCinematicController);
            this.StartCinematicMode();
        }

        private void EngageConditionSimulate()
        {
            this.Constructor.animator.Play(this.Constructor.engageCinematicController.animParam, 0, 1f);
            this.Constructor.animator.SetBool(this.Constructor.engageCinematicController.animParam, true);

            this.PlayerAnimator.speed = 99f;
            this.PlayerAnimator.SetTrigger(this.Constructor.engageCinematicController.playerViewAnimationName);

            for (int i = 0; i < 10; i++)
            {
                this.PlayerAnimator.Update(Time.deltaTime == 0f ? 0.01f : Time.deltaTime);
            }

            this.PlayerAnimator.speed = 1f;
        }
    }
}
