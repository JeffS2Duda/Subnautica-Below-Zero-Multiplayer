namespace Subnautica.Client.Synchronizations.InitialSync
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Core;
    using Subnautica.Client.Extensions;
    using Subnautica.Client.MonoBehaviours.World;
    using Subnautica.Network.Models.Storage.Player;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using ServerModel = Subnautica.Network.Models.Server;

    public class PlayerProcessor
    {
        public static void OnPlayerInitialized()
        {
            if (Network.Session.Current.PlayerHealth < 2f)
            {
                Network.Session.Current.PlayerHealth = 2f;
            }

            if (Network.Session.Current.PlayerUsedTools?.Count > 0)
            {
                ZeroPlayer.CurrentPlayer.Main.usedTools.AddRange(Network.Session.Current.PlayerUsedTools);
            }

            ZeroPlayer.CurrentPlayer.Main.oxygenMgr.Restore();
            ZeroPlayer.CurrentPlayer.Main.timeLastSleep = Network.Session.Current.PlayerTimeLastSleep;
            ZeroPlayer.CurrentPlayer.Main.liveMixin.health = Network.Session.Current.PlayerHealth;
            ZeroPlayer.CurrentPlayer.Main.GetComponent<Survival>().food = Network.Session.Current.PlayerFood;
            ZeroPlayer.CurrentPlayer.Main.GetComponent<Survival>().water = Network.Session.Current.PlayerWater;

            if (Network.Session.Current.InteractList != null)
            {
                Interact.SetList(Network.Session.Current.InteractList);
            }

            Inventory.main.SecureItems(true);

            ZeroPlayer.CurrentPlayer.OnCurrentPlayerLoaded();
        }

        public static void OnPlayerFirstEquipmentInitialized()
        {
            if (GameModeManager.GetOption<bool>(GameOption.InitialEquipmentPack) && !Network.Session.Current.IsInitialEquipmentAdded)
            {
                NetworkClient.SendPacket(new ServerModel.PlayerInitialEquipmentArgs());
            }
        }

        public static void OnPlayerPositionInitialized()
        {
            if (Network.Session.Current.PlayerPosition != null)
            {
                ZeroPlayer.CurrentPlayer.Main.transform.position = Network.Session.Current.PlayerPosition.ToVector3();
                ZeroPlayer.CurrentPlayer.Main.lastPosition = Network.Session.Current.PlayerPosition.ToVector3();
                ZeroPlayer.CurrentPlayer.Main.transform.rotation = Network.Session.Current.PlayerRotation.ToQuaternion();
            }

            if (Vector3.zero == ZeroPlayer.CurrentPlayer.Main.transform.position)
            {
                var gameData = Player.main.GetGameData(SaveLoadManager.main.storyVersion);
                if (Network.Session.Current.GameMode == GameModePresetId.Creative)
                {
                    Player.main.SetPosition(gameData.creativeStartLocation.position, Quaternion.Euler(gameData.creativeStartLocation.rotation));
                }
                else
                {
                    Player.main.SetPosition(gameData.storyStartLocation.position, Quaternion.Euler(gameData.storyStartLocation.rotation));
                }
            }
        }

        public static void OnPlayerSubRootInitialized()
        {
            if (Network.Session.Current.PlayerSubRootId.IsNotNull())
            {
                using (EventBlocker.Create(ProcessType.InteriorToggle))
                using (EventBlocker.Create(ProcessType.SubrootToggle))
                {
                    var subroot = Network.Identifier.GetComponentByGameObject<SubRoot>(Network.Session.Current.PlayerSubRootId, true);
                    if (subroot != null)
                    {
                        ZeroPlayer.CurrentPlayer.Main.SetCurrentSub(subroot);
                        ZeroPlayer.CurrentPlayer.Main.playerController.SetEnabled(true);
                    }
                }
            }
            else if (Network.Session.Current.PlayerInteriorId.IsNotNull())
            {
                global::SeaTruckSegment newSeaTruckSegment = null;

                using (EventBlocker.Create(ProcessType.InteriorToggle))
                using (EventBlocker.Create(ProcessType.SubrootToggle))
                using (EventBlocker.Create(ProcessType.VehicleEnter))
                {
                    var interior = Network.Identifier.GetComponentByGameObject<global::IInteriorSpace>(Network.Session.Current.PlayerInteriorId);
                    if (interior != null)
                    {
                        var respawnPoint = interior.GetRespawnPoint();
                        if (respawnPoint)
                        {
                            ZeroPlayer.CurrentPlayer.Main.SetPosition(respawnPoint.GetSpawnPosition());

                            if (interior.GetGameObject().TryGetComponent<global::SeaTruckSegment>(out var seaTruckSegment))
                            {
                                if (seaTruckSegment == seaTruckSegment.GetFirstSegment())
                                {
                                    seaTruckSegment.Enter(ZeroPlayer.CurrentPlayer.Main);
                                }
                                else
                                {
                                    newSeaTruckSegment = seaTruckSegment.GetFirstSegment();
                                }
                            }
                            else
                            {
                                ZeroPlayer.CurrentPlayer.Main.EnterInterior(interior);
                            }
                        }
                        else
                        {
                            ZeroPlayer.CurrentPlayer.Main.EnterInterior(interior);
                        }
                    }
                }

                if (newSeaTruckSegment)
                {
                    newSeaTruckSegment.GetFirstSegment().Enter(ZeroPlayer.CurrentPlayer.Main);
                }
            }
            if (ZeroPlayer.CurrentPlayer.Main.currentSub)
            {
                foreach (WaterPark componentsInChild in ZeroPlayer.CurrentPlayer.Main.currentSub.GetComponentsInChildren<WaterPark>(true))
                {
                    if (componentsInChild is LargeRoomWaterPark largeRoomWaterPark)
                    {
                        if (largeRoomWaterPark.IsPointInside(ZeroPlayer.CurrentPlayer.Main.transform.position))
                        {
                            ZeroPlayer.CurrentPlayer.Main.currentWaterPark = componentsInChild;
                            break;
                        }
                    }
                    else if (componentsInChild.IsPointInside(ZeroPlayer.CurrentPlayer.Main.transform.position))
                    {
                        ZeroPlayer.CurrentPlayer.Main.currentWaterPark = componentsInChild;
                        break;
                    }
                }
            }
            PlayerProcessor.FixMoonpoolExpansionPlayerPosition(ZeroPlayer.CurrentPlayer.Main, Network.Session.Current.PlayerSubRootId);
        }

        public static void OnPlayerRespawnPointInitialized()
        {
            if (Network.Session.Current.SupplyDrops?.Count > 0)
            {
                foreach (var supplyDrop in Network.Session.Current.SupplyDrops.Where(q => q.ZoneId != -1))
                {
                    ZeroPlayer.CurrentPlayer.Main.fallbackRespawnInteriorUID = supplyDrop.UniqueId;
                }
            }

            if (Network.Session.Current.PlayerRespawnPointId.IsNotNull() && Network.Identifier.GetGameObject(Network.Session.Current.PlayerRespawnPointId, true) != null)
            {
                ZeroPlayer.CurrentPlayer.Main.currentRespawnInteriorUID = Network.Session.Current.PlayerRespawnPointId;
            }
        }

        public static void OnOtherPlayersInitialized(List<PlayerItem> players)
        {
            if (players?.Count > 0)
            {
                foreach (var packet in players)
                {
                    var player = ZeroPlayer.CreateOrGetPlayerByUniqueId(packet.UniqueId, packet.PlayerId);
                    player.SetPlayerName(packet.PlayerName);
                    player.SetSubRootId(packet.SubrootId);
                    player.SetInteriorId(packet.InteriorId);

                    if (!player.IsCreatedModel)
                    {
                        player.CreateModel(packet.Position.ToVector3(true), packet.Rotation.ToQuaternion(true));
                        player.InitBehaviours();
                    }
                }
            }
        }

        private static bool FixMoonpoolExpansionPlayerPosition(global::Player player, string subrootId)
        {
            var firstSegment = player.GetUnderGameObject<global::SeaTruckSegment>()?.GetFirstSegment();
            if (firstSegment)
            {
                var moonpoolExpansionManager = firstSegment.GetDockedMoonpoolExpansion(true);
                if (moonpoolExpansionManager)
                {
                    var dockingBay = moonpoolExpansionManager.bay.gameObject.EnsureComponent<MultiplayerVehicleDockingBay>();
                    if (dockingBay && dockingBay.ExpansionManager.IsPlayerInMoonpoolExpansion())
                    {
                        firstSegment.Exit(skipAnimations: true, newInterior: moonpoolExpansionManager.interior);

                        player.SetPosition(dockingBay.ExpansionManager.GetTerminalSpawnPosition());
                        return true;
                    }
                }
            }

            player.UpdateIsUnderwater();

            if (subrootId.IsNotNull())
            {
                var subroot = Network.Identifier.GetComponentByGameObject<SubRoot>(subrootId, true);
                if (subroot)
                {
                    foreach (var vehicleDockingBay in subroot.GetComponentsInChildren<global::VehicleDockingBay>())
                    {
                        var dockingBay = vehicleDockingBay.gameObject.EnsureComponent<MultiplayerVehicleDockingBay>();
                        if (dockingBay && dockingBay.ExpansionManager.IsActive() && dockingBay.ExpansionManager.IsPlayerInMoonpoolExpansion())
                        {
                            player.SetPosition(dockingBay.ExpansionManager.GetTerminalSpawnPosition());
                            return true;
                        }
                    }
                }
            }
            else if (player.IsUnderwater())
            {
                foreach (var vehicleDockingBay in GameObject.FindObjectsOfType<global::VehicleDockingBay>())
                {
                    var dockingBay = vehicleDockingBay.gameObject.EnsureComponent<MultiplayerVehicleDockingBay>();
                    if (dockingBay && dockingBay.ExpansionManager.IsActive() && dockingBay.ExpansionManager.IsPlayerInMoonpoolExpansion())
                    {
                        player.SetPosition(dockingBay.ExpansionManager.GetOutsideSpawnPosition());
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
