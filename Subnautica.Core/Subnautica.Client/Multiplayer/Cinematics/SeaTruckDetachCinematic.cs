namespace Subnautica.Client.Multiplayer.Cinematics
{
    using Subnautica.API.Extensions;
    using Subnautica.Client.MonoBehaviours.Player;

    public class SeaTruckDetachCinematic : CinematicController
    {
        private global::SeaTruckMotor SeaTruckMotor { get; set; }

        public override void OnResetAnimations(PlayerCinematicQueueItem item)
        {
            this.SeaTruckMotor = this.Target.GetComponentInChildren<global::SeaTruckMotor>();
            this.SeaTruckMotor.animator.Rebind();
        }

        public void DetachSeaTruckCinematic()
        {
            if (this.SeaTruckMotor.seatruckanimation)
            {
                this.SetCinematic(this.SeaTruckMotor.seatruckanimation.GetController(SeaTruckAnimation.Animation.EjectModules));
                this.SetCinematicEndMode(this.DetachSeaTruckEndMode);
                this.StartCinematicMode();
            }
        }

        private void DetachSeaTruckEndMode()
        {
            if (this.ZeroPlayer.IsInSeaTruck)
            {
                this.ZeroPlayer.GetComponent<PlayerAnimation>().UpdateIsInSeaTruck(true);
            }
        }
    }
}
