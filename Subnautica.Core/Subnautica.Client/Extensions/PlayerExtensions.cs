namespace Subnautica.Client.Extensions
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.MonoBehaviours.Player;
    using Subnautica.Network.Models.Server;
    using Subnautica.Network.Structures;

    using UnityEngine;

    public static class PlayerExtensions
    {
        public static void InitBehaviours(this ZeroPlayer player)
        {
            if (player?.PlayerModel != null)
            {
                player.EnsureComponent<PlayerLighting>().Player = player;
                player.EnsureComponent<PlayerAnimation>().Player = player;
                player.EnsureComponent<PlayerEquipment>().Player = player;
                player.EnsureComponent<PlayerHandItemManager>().Player = player;
                player.EnsureComponent<PlayerVehicleManagement>().Player = player;
                player.EnsureComponent<PlayerFootstepSounds>().Player = player;

                var playerEquipment = player.GetComponent<PlayerEquipment>();
                playerEquipment.ResetEquipments();
            }
        }

        public static T GetHandTool<T>(this ZeroPlayer player, TechType techType)
        {
            var itemManager = player.GetComponent<PlayerHandItemManager>();
            if (itemManager == null)
            {
                return default(T);
            }

            var item = itemManager.GetItem(techType);
            if (item == null)
            {
                return default(T);
            }

            var tool = item.GetComponent<T>();
            if (tool == null)
            {
                return default(T);
            }

            return tool;
        }

        public static void SetHandItem(this ZeroPlayer player, TechType techType)
        {
            var itemManager = player.GetComponent<PlayerHandItemManager>();
            if (itemManager)
            {
                if (techType == TechType.None)
                {
                    itemManager.ClearHand();
                }
                else
                {
                    itemManager.SetHand(techType);
                }
            }
        }

        public static bool RefreshVehicle(this ZeroPlayer player, ushort vehicleId)
        {
            var entity = Network.DynamicEntity.GetEntity(vehicleId);
            if (entity == null)
            {
                return false;
            }

            player.SetVehicle(entity.Id, entity.Position, entity.Rotation, null);

            var vehicleManagement = player.GetComponent<PlayerVehicleManagement>();
            if (vehicleManagement)
            {
                vehicleManagement.RefreshVehicle(entity.Id, entity.TechType, false);
                vehicleManagement.VehicleUniqueId = null;
                vehicleManagement.OnEnterVehicle();
                vehicleManagement.VehicleUniqueId = entity.UniqueId;
            }

            return true;
        }

        public static bool SetVehicle(this ZeroPlayer player, ushort vehicleId, ZeroVector3 position, ZeroQuaternion rotation, VehicleUpdateComponent component)
        {
            var entity = Network.DynamicEntity.GetEntity(vehicleId);
            if (entity == null)
            {
                return false;
            }

            player.VehicleId = entity.Id;
            player.VehicleType = entity.TechType;
            player.VehiclePosition = position.ToVector3();
            player.VehicleRotation = rotation.ToQuaternion();
            player.VehicleComponent = component;

            if (player.VehicleComponent != null)
            {
                player.VehicleComponent.IsNew = true;
            }

            if (entity.TechType != TechType.SpyPenguin && entity.TechType != TechType.MapRoomCamera)
            {
                player.Position = player.VehiclePosition;
                player.Rotation = player.VehicleRotation;
            }

            entity.Position = position;
            entity.Rotation = rotation;
            return true;
        }

        public static void ExitVehicle(this ZeroPlayer player)
        {
            if (player.VehicleId > 0)
            {
                player.GetComponent<PlayerVehicleManagement>()?.OnExitVehicle();
            }

            player.ClearVehicle();
        }

        public static void ClearVehicle(this ZeroPlayer player)
        {
            player.VehicleId = 0;
            player.VehicleType = TechType.None;
        }

        public static T EnsureComponent<T>(this ZeroPlayer player) where T : MonoBehaviour
        {
            return player.PlayerModel.EnsureComponent<T>();
        }

        public static T GetCinematic<T>(this ZeroPlayer player) where T : CinematicController
        {
            if (player.PlayerModel == null)
            {
                return default(T);
            }

            if (player.PlayerModel.TryGetComponent<T>(out T cinematic))
            {
                return cinematic;
            }

            cinematic = player.EnsureComponent<T>();
            cinematic.Initialize(player);
            return cinematic;
        }

        public static CinematicController[] GetCinematics(this ZeroPlayer player)
        {
            if (player.PlayerModel == null)
            {
                return new CinematicController[0];
            }

            return player.PlayerModel.GetComponents<CinematicController>();
        }
    }
}
