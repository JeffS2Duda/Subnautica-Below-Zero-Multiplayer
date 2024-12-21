namespace Subnautica.Client.Multiplayer.Cinematics
{
    using Subnautica.Client.MonoBehaviours.Player;

    public class BulkheadDoorCinematic : CinematicController
    {
        private global::BulkheadDoor Door { get; set; }

        public override void OnResetAnimations(PlayerCinematicQueueItem item)
        {
            this.Door = this.Target.GetComponentInChildren<global::BulkheadDoor>();
        }

        public void OpenDoorStartCinematic()
        {
            var doorSide = this.GetProperty<bool>("Side");

            this.Door.animator.SetBool(global::BulkheadDoor.animPlayerInFront, doorSide);

            this.RegisterProperty("IsOpen", true);
            this.SetCinematic(this.GetCinematic(doorSide, true));
            this.SetCinematicEndMode(this.CinematicModeEnd);
            this.StartCinematicMode();
        }

        public void CloseDoorStartCinematic()
        {
            var doorSide = this.GetProperty<bool>("Side");

            this.Door.animator.SetBool(global::BulkheadDoor.animPlayerInFront, doorSide);

            this.RegisterProperty("IsOpen", false);
            this.SetCinematic(this.GetCinematic(doorSide, false));
            this.SetCinematicEndMode(this.CinematicModeEnd);
            this.StartCinematicMode();
        }

        private void CinematicModeEnd()
        {
            this.Door.SetState(this.GetProperty<bool>("IsOpen"));
        }

        private global::PlayerCinematicController GetCinematic(bool side, bool isOpen)
        {
            if (!side)
            {
                if (!this.Door.shouldPassThrough || !this.Door.backPassthroughCinematicController)
                {
                    return isOpen ? this.Door.backOpenCinematicController : this.Door.backCloseCinematicController;
                }

                return this.Door.backPassthroughCinematicController;
            }
            else
            {
                if (!this.Door.shouldPassThrough || !this.Door.backPassthroughCinematicController)
                {
                    return isOpen ? this.Door.frontOpenCinematicController : this.Door.frontCloseCinematicController;
                }

                return this.Door.frontPassthroughCinematicController;
            }
        }
    }
}
