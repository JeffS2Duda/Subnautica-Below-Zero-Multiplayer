namespace Subnautica.Client.Multiplayer.Vehicles
{
    public class SeaTruckModule : VehicleController
    {
        private global::SeaTruckMotor Vehicle { get; set; }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnEnterVehicle()
        {
            base.OnEnterVehicle();

            if (this.Management.Vehicle)
            {
                this.Vehicle = this.Management.Vehicle.GetComponent<global::SeaTruckMotor>();

                this.SetPlayerParent(this.Vehicle.pilotPosition.transform);

                this.Vehicle.truckSegment.animator.SetBool("piloting", true);
                this.Management.Player.Animator.SetBool("seatruck_pushing", true);
            }
        }

        public override void OnExitVehicle()
        {
            if (this.Vehicle)
            {
                this.Vehicle.truckSegment.animator.SetBool("piloting", false);
            }
            
            this.Management.Player.Animator.SetBool("seatruck_pushing", false);

            base.OnExitVehicle();
        }
    }
}
