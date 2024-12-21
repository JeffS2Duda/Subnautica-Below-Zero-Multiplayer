namespace Subnautica.Client.Multiplayer.Vehicles
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Extensions;
    using Subnautica.Client.MonoBehaviours.Player;
    using Subnautica.Network.Models.Server;
    using Subnautica.Network.Structures;

    using UnityEngine;

    public class VehicleController
    {
        public virtual void OnAwake(PlayerVehicleManagement management)
        {
            this.Management = management;
        }

        public virtual void OnComponentDataReceived(VehicleUpdateComponent component)
        {

        }

        public virtual void OnUpdate()
        {
            if (this.IsDefaultInterpolate)
            {
                this.InterpolateVehicle();
            }
        }

        public virtual void OnFixedUpdate()
        {

        }

        public virtual void OnEnterVehicle()
        {
            if (this.Management.Player != null && this.Management.VehicleUniqueId.IsNotNull())
            {
                this.Management.Player.ResetCinematicsByUniqueId(this.Management.VehicleUniqueId);
                this.Management.Player.ResetCinematics();
            }

            if (this.Management.Vehicle)
            {
                if (this.Management.Vehicle.TryGetComponent<EcoTarget>(out var ecoTarget))
                {
                    ecoTarget.enabled = true;
                }

                if (this.Management.Vehicle.TryGetComponent<WorldForces>(out var worldForces))
                {
                    worldForces.enabled = false;
                }

                if (this.Management.Vehicle.TryGetComponent<global::PingInstance>(out var pingInstance))
                {
                    pingInstance.SetVisible(false);
                }
            }
        }

        public virtual void OnExitVehicle()
        {
            if (this.Management.Vehicle)
            {
                if (this.Management.Vehicle.TryGetComponent<EcoTarget>(out var ecoTarget))
                {
                    ecoTarget.enabled = false;
                }

                if (this.Management.Vehicle.TryGetComponent<WorldForces>(out var worldForces))
                {
                    worldForces.enabled = true;
                }

                if (this.Management.Vehicle.TryGetComponent<global::PingInstance>(out var pingInstance))
                {
                    pingInstance.SetVisible(true);
                }
            }

            this.SetPlayerParent(null);
        }

        public bool SetPlayerParent(Transform transform)
        {
            if (this.Management.Player == null || this.Management.Player.PlayerModel == null)
            {
                return false;
            }

            if (transform)
            {
                this.Management.Player.PlayerModel.transform.parent        = transform;
                this.Management.Player.PlayerModel.transform.localPosition = Vector3.zero;
                this.Management.Player.PlayerModel.transform.localRotation = Quaternion.identity;
            }
            else
            {
                this.Management.Player.PlayerModel.transform.parent = null;
                this.Management.Player.PlayerModel.transform.localScale = Vector3.one;
            }

            if (this.Management.Player.PlayerModel.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                rigidbody.SetInterpolation(RigidbodyInterpolation.None);
                rigidbody.SetKinematic();
            }

            return true;
        }

        public virtual void InterpolateVehicle()
        {
            if (this.Management.Vehicle)
            {
                if (this.GetDistance() > 100f)
                {
                    this.Management.Vehicle.transform.position = this.Management.Player.VehiclePosition;
                    this.Management.Vehicle.transform.rotation = this.Management.Player.VehicleRotation;
                }
                else
                {
                    this.Management.Vehicle.transform.position = Vector3.SmoothDamp(this.Management.Vehicle.transform.position, this.Management.Player.VehiclePosition, ref this.VehicleVelocity, 0.1f);
                    this.Management.Vehicle.transform.rotation = BroadcastInterval.QuaternionSmoothDamp(this.Management.Vehicle.transform.rotation, this.Management.Player.VehicleRotation, ref this.VehicleRotationVelocity, 0.1f);
                }

                this.Management.Player.SetVelocity(this.VehicleVelocity);
            }
        }

        public float GetDistance()
        {
            return ZeroVector3.Distance(this.Management.Vehicle.transform.position, this.Management.Player.VehiclePosition);
        }

        public Vector3 GetVelocity()
        {
            return this.VehicleVelocity;
        }

        public PlayerVehicleManagement Management { get; set; }

        public bool IsDefaultInterpolate { get; set; } = true;

        public Vector3 VehicleVelocity = Vector3.zero;

        public Quaternion VehicleRotationVelocity;
    }
}
