namespace Subnautica.Client.MonoBehaviours.Player
{
    using Subnautica.API.Features;
    using Subnautica.Client.Extensions;
    using Subnautica.Client.Multiplayer.Vehicles;
    using Subnautica.Network.Models.Server;
    using System.Collections.Generic;
    using UnityEngine;
    using VehicleModel = Subnautica.Client.Multiplayer.Vehicles;

    public class PlayerVehicleManagement : MonoBehaviour
    {
        public ZeroPlayer Player { get; set; }

        public GameObject Vehicle { get; private set; }

        private TechType VehicleType { get; set; } = TechType.None;

        private ushort VehicleId { get; set; } = 0;

        public string VehicleUniqueId { get; set; }

        public Dictionary<TechType, VehicleModel.VehicleController> Vehicles { get; private set; } = new Dictionary<TechType, VehicleModel.VehicleController>()
        {
            { TechType.Hoverbike                    , new VehicleModel.Hoverbike() },
            { TechType.Exosuit                      , new VehicleModel.Exosuit() },
            { TechType.SeaTruck                     , new VehicleModel.SeaTruck() },
            { TechType.SpyPenguin                   , new VehicleModel.SpyPenguin() },
            { TechType.MapRoomCamera                , new VehicleModel.MapRoomCamera() },
            { TechType.SeaTruckFabricatorModule     , new VehicleModel.SeaTruckModule() },
            { TechType.SeaTruckStorageModule        , new VehicleModel.SeaTruckModule() },
            { TechType.SeaTruckAquariumModule       , new VehicleModel.SeaTruckModule() },
            { TechType.SeaTruckDockingModule        , new VehicleModel.SeaTruckModule() },
            { TechType.SeaTruckSleeperModule        , new VehicleModel.SeaTruckModule() },
            { TechType.SeaTruckTeleportationModule  , new VehicleModel.SeaTruckModule() },
        };

        public void Start()
        {
            foreach (var vehicle in this.Vehicles)
            {
                vehicle.Value.OnAwake(this);
            }
        }

        public VehicleModel.VehicleController GetCurrentVehicle()
        {
            if (this.VehicleType != TechType.None && this.Vehicles.TryGetValue(this.VehicleType, out var vehicle))
            {
                return vehicle;
            }

            return null;
        }

        public void Update()
        {
            if (this.Player != null && this.Player.IsCreatedModel && !this.Player.IsVehicleDocking && World.IsLoaded)
            {
                this.RefreshVehicle(this.Player.VehicleId, this.Player.VehicleType);

                if (this.VehicleType != TechType.None && this.Vehicles.TryGetValue(this.VehicleType, out var vehicle))
                {
                    this.RefreshComponent(vehicle, this.Player.VehicleComponent);

                    vehicle.OnUpdate();
                }
            }
        }

        public void FixedUpdate()
        {
            if (this.Player != null && this.Player.IsCreatedModel && !this.Player.IsVehicleDocking && World.IsLoaded)
            {
                this.RefreshVehicle(this.Player.VehicleId, this.Player.VehicleType);

                if (this.VehicleType != TechType.None && this.Vehicles.TryGetValue(this.VehicleType, out var vehicle))
                {
                    vehicle.OnFixedUpdate();
                }
            }
        }

        public bool RefreshVehicle(ushort vehicleId, TechType vehicleType, bool isEnter = true)
        {
            if (this.VehicleId == vehicleId)
            {
                return false;
            }

            if (this.VehicleId > 0)
            {
                this.OnExitVehicle();
            }

            if (vehicleId <= 0)
            {
                return true;
            }

            var entity = Network.DynamicEntity.GetEntity(vehicleId);
            if (entity == null)
            {
                return false;
            }

            this.VehicleUniqueId = entity.UniqueId;

            Network.DynamicEntity.SetEntityUsingByPlayer(vehicleId, true);

            this.Vehicle = Network.Identifier.GetGameObject(entity.UniqueId);
            this.VehicleId = vehicleId;
            this.VehicleType = vehicleType;

            if (isEnter)
            {
                this.OnEnterVehicle();
            }

            return true;
        }

        private void RefreshComponent(VehicleController vehicle, VehicleUpdateComponent component)
        {
            if (component != null && component.IsNew)
            {
                component.IsNew = false;

                vehicle.OnComponentDataReceived(component);
            }
        }

        public void OnEnterVehicle()
        {
            if (this.Vehicles.TryGetValue(this.VehicleType, out var vehicle))
            {
                this.Player.ResetEmotes();
                this.Player.ResetAnimations();
                this.Player.SetHandItem(TechType.None);

                vehicle.OnEnterVehicle();
            }
        }

        public void OnExitVehicle()
        {
            Network.DynamicEntity.SetEntityUsingByPlayer(this.VehicleId, false);

            if (this.Vehicle)
            {
                this.Vehicle.transform.position = this.Player.VehiclePosition;
                this.Vehicle.transform.rotation = this.Player.VehicleRotation;
            }

            if (this.VehicleType != TechType.None && this.Vehicles.TryGetValue(this.VehicleType, out var vehicle))
            {
                vehicle.OnExitVehicle();
            }

            this.Vehicle = null;
            this.VehicleUniqueId = null;
            this.VehicleId = 0;
            this.VehicleType = TechType.None;
        }

        public void OnDestroy()
        {
            this.OnExitVehicle();

            this.Vehicles.Clear();
        }
    }
}
