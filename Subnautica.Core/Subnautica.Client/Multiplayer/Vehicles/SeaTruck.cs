namespace Subnautica.Client.Multiplayer.Vehicles
{
    using Subnautica.API.Extensions;
    using Subnautica.Client.Extensions;

    public class SeaTruck : VehicleController
    {
        private global::SeaTruckMotor Vehicle { get; set; }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (this.Vehicle)
            {
                this.UpdateSounds();
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (!this.Vehicle.ecoTarget.enabled)
            {
                ErrorMessage.AddMessage("FALSE ECO TARGET!");
            }
        }

        private void UpdateSounds()
        {
        }

        public override void OnEnterVehicle()
        {
            base.OnEnterVehicle();

            if (this.Management.Vehicle)
            {
                this.Vehicle = this.Management.Vehicle.GetComponent<global::SeaTruckMotor>();
                this.Vehicle.truckSegment.GetFirstSegment().rb.SetKinematic();

                this.SetPlayerParent(this.Vehicle.pilotPosition.transform);

                if (this.Management.VehicleUniqueId.IsNotNull())
                {
                    this.Management.Player.SeaTruckStartPilotingCinematic(this.Management.VehicleUniqueId);
                }
            }
        }

        public override void OnExitVehicle()
        {
            if (this.Management.Vehicle)
            {
                this.Vehicle = null;
            }

            if (this.Management.VehicleUniqueId.IsNotNull())
            {
                this.Management.Player.SeaTruckStopPilotingCinematic(this.Management.VehicleUniqueId);
            }

            base.OnExitVehicle();
        }
    }
}
