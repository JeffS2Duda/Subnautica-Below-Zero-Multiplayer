namespace Subnautica.Client.Multiplayer.Cinematics
{
    using Subnautica.Client.MonoBehaviours.Player;

    public class ClimbCinematic : CinematicController
    {
        private global::CinematicModeTriggerBase Ladder;

        private global::CinematicModeTrigger ConstructorLadder;

        public override void OnResetAnimations(PlayerCinematicQueueItem item)
        {
            if (!this.Target.TryGetComponent<global::CinematicModeTriggerBase>(out this.Ladder))
            {
                this.ConstructorLadder = this.Target.GetComponentInChildren<global::CinematicModeTrigger>();
            }
        }

        public void ClimbStartCinematic()
        {
            if (this.Ladder)
            {
                this.SetCinematic(this.Ladder.cinematicController);
            }

            if (this.ConstructorLadder)
            {
                this.SetCinematic(this.ConstructorLadder.cinematicController);
            }

            if (this.Ladder || this.ConstructorLadder)
            {
                this.StartCinematicMode();
            }
        }
    }
}
