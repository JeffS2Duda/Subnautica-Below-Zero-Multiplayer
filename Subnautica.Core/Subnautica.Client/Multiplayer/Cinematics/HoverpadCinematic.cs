namespace Subnautica.Client.Multiplayer.Cinematics
{
    using Subnautica.API.Extensions;
    using Subnautica.Client.Extensions;
    using Subnautica.Client.MonoBehaviours.Player;

    public class HoverpadCinematic : CinematicController
    {
        private global::Hoverpad Hoverpad { get; set; }

        public override void OnResetAnimations(PlayerCinematicQueueItem item)
        {
            this.Hoverpad = this.Target.GetComponent<global::Hoverpad>();

            if (item.CinematicAction == this.UndockStartCinematic)
            {
                this.Hoverpad.animator.FastPlay("Base Layer.park bike");
            }
        }

        public void DockStartCinematic()
        {
            if (this.Hoverpad && this.Hoverpad.dockedBike)
            {
                this.ZeroPlayer.IsVehicleDocking = true;
                this.ZeroPlayer.ExitVehicle();

                this.Hoverpad.dockedBike.animator.SetBool("park_bike", true);

                this.SetCinematic(this.Hoverpad.dockCinematic);
                this.SetCinematicEndMode(this.DockCinematicEndMode);
                this.StartCinematicMode();
            }
        }

        public void UndockStartCinematic()
        {
            var trigger = this.Hoverpad.GetComponentInChildren<HoverpadUndockTrigger>();
            if (trigger && trigger.hoverpad.isBikeDocked)
            {
                trigger.hoverpad.StartBikeUndockSequence(trigger.forwardAnimParamToBike);

                this.RegisterProperty("Animation", trigger.forwardAnimParamToBike);
                this.SetCinematic(trigger.cinematicController);
                this.SetCinematicEndMode(this.UndockCinematicEndMode);
                this.StartCinematicMode();
            }
        }

        private void UndockCinematicEndMode()
        {
            if (this.Hoverpad)
            {
                this.Hoverpad.dockedBike.UndockFromHoverpad();
                this.Hoverpad.dockedBike.transform.SetParent(null);
                this.Hoverpad.dockedBike.animator.SetBool(this.GetProperty<string>("Animation"), false);

                if (this.ZeroPlayer != null)
                {
                    this.ZeroPlayer.VehiclePosition = this.Hoverpad.dockedBike.transform.position;
                    this.ZeroPlayer.VehicleRotation = this.Hoverpad.dockedBike.transform.rotation;
                }

                this.Hoverpad.terminalGUI.SetScreen();
                this.Hoverpad.terminalGUI.SetCustomizeable(null);
                this.Hoverpad.SetBikeTriggersImmediate(false);
                this.Hoverpad.allowUndock = true;
            }
        }

        private void DockCinematicEndMode()
        {
            this.ZeroPlayer.IsVehicleDocking = false;

            if (this.Hoverpad)
            {
                this.Hoverpad.dockedBike.animator.SetBool("park_bike", false);
            }
        }
    }
}
