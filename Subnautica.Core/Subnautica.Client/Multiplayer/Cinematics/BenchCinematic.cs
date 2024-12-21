namespace Subnautica.Client.Multiplayer.Cinematics
{
    using Subnautica.Client.MonoBehaviours.Player;

    using UnityEngine;

    public class BenchCinematic : CinematicController
    {
        private global::Bench Bench { get; set; }

        public override void OnResetAnimations(PlayerCinematicQueueItem item)
        {
            this.Bench = this.Target.GetComponent<global::Bench>();
            this.Bench.animator.Rebind();
        }

        public void SitDownStartCinematic()
        {
            this.Bench.animator.transform.localEulerAngles = this.GetPlayerSitdownAngles(this.GetProperty<global::Bench.BenchSide>("Side"));
            this.Bench.ResetAnimParams(this.PlayerAnimator);

            this.SetCinematic(this.Bench.cinematicController);
            this.StartCinematicMode();
        }

        public void StandupStartCinematic()
        {
            this.Bench.animator.transform.localEulerAngles = this.GetPlayerSitdownAngles(this.GetProperty<global::Bench.BenchSide>("Side"));
            this.Bench.ResetAnimParams(this.PlayerAnimator);

            this.SetCinematic(this.Bench.standUpCinematicController, true);
            this.PlayerViewAnimationName = this.GetPlayerStandupAnimationName();
            this.StartCinematicMode();
        }

        private Vector3 GetPlayerSitdownAngles(global::Bench.BenchSide side)
        {
            return side == global::Bench.BenchSide.Front ? this.Bench.frontAnimRotation : this.Bench.backAnimRotation;
        }

        private string GetPlayerStandupAnimationName()
        {
            if (this.Target.TryGetComponent<Constructable>(out var constructable) && constructable.techType == TechType.Bench)
            {
                return "stand_up";
            }

            return "chair_stand_up";
        }
    }
}
