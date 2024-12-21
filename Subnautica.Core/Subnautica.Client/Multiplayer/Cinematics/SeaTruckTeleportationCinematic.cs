namespace Subnautica.Client.Multiplayer.Cinematics
{
    using Subnautica.API.Extensions;
    using Subnautica.Client.MonoBehaviours.Player;

    public class SeaTruckTeleportationCinematic : CinematicController
    {
        private global::SeaTruckTeleporter Teleporter { get; set; }

        public override void OnResetAnimations(PlayerCinematicQueueItem item)
        {
            this.Teleporter = this.Target.GetComponentInChildren<global::SeaTruckTeleporter>();
        }

        public void SeaTruckTeleportationStartCinematic()
        {
            this.Teleporter.arrivalVFX.Play();

            this.SetCinematic(this.Teleporter.arrivalCinematic);
            this.SetCinematicEndMode(this.TeleportationEnd);
            this.StartCinematicMode();
        }

        private void TeleportationEnd()
        {
            if (this.Teleporter && this.ZeroPlayer.IsInSeaTruck)
            {
                this.ZeroPlayer.GetComponent<PlayerAnimation>().UpdateIsInSeaTruck(true);
            }
        }
    }
}
