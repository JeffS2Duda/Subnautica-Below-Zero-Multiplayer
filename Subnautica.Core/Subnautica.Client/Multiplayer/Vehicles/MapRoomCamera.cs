namespace Subnautica.Client.Multiplayer.Vehicles
{
    using Subnautica.API.Extensions;

    public class MapRoomCamera : VehicleController
    {
        private global::MapRoomCamera Camera { get; set; }

        public override void OnEnterVehicle()
        {
            base.OnEnterVehicle();

            if (this.Management.Vehicle)
            {
                this.Camera = this.Management.Vehicle.GetComponent<global::MapRoomCamera>();
                this.Camera.rigidBody.SetKinematic();
            }
        }

        public override void OnExitVehicle()
        {
            this.Management.Player.ResetAnimations();
            this.Management.Player.SetUsingRoomId(null);

            if (this.Management.Vehicle)
            {
                this.Camera.engineSound.Stop();
                this.Camera = null;
            }

            base.OnExitVehicle();
        }
    }
}