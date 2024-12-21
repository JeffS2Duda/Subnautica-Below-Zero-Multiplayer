namespace Subnautica.Client.Synchronizations.Processors.General
{
    using global::Story;
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using System;
    using System.Linq;
    using ServerModel = Subnautica.Network.Models.Server;

    public class LifepodProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.LifepodArgs>();
            if (packet == null)
            {
                return true;
            }

            if (packet.IsAdded)
            {
                Network.Storage.AddItemToStorage(packet.UniqueId, packet.GetPacketOwnerId(), packet.WorldPickupItem);
            }
            else
            {
                Network.Storage.AddItemToInventory(packet.GetPacketOwnerId(), packet.WorldPickupItem);
            }

            return true;
        }

        public static void OnLifepodZoneSelecting(LifepodZoneSelectingEventArgs ev)
        {
            var lifePod = Network.Session.Current.SupplyDrops.Where(q => q.Key == ev.Key).FirstOrDefault();
            if (lifePod != null)
            {
                ev.IsAllowed = false;

                if (lifePod.ZoneId != -1)
                {
                    ev.ZoneId = lifePod.ZoneId;
                }
            }
        }

        public static void OnLifepodZoneCheck(LifepodZoneCheckEventArgs ev)
        {
            if (Network.Session.Current.SupplyDrops.Where(q => q.Key == ev.Key).Any())
            {
                ev.IsAllowed = false;
            }
        }

        public static void OnLifepodInterpolation(LifepodInterpolationEventArgs ev)
        {
            try
            {
                var key = ev.DropObject.GetComponent<ISupplyDrop>().GetDropData().precondition;
                if (key != null)
                {
                    var lifePod = Network.Session.Current.SupplyDrops.Where(q => q.Key == key).FirstOrDefault();
                    if (lifePod != null)
                    {
                        ev.IsAllowed = false;
                        ev.IsCompleted = lifePod.IsCompleted(DayNightCycle.main.timePassedAsFloat);
                        ev.Rotation = lifePod.Rotation.ToQuaternion();
                        ev.StartedTime = lifePod.StartedTime;

                        Network.Identifier.SetIdentityId(ev.DropObject, lifePod.UniqueId);

                        var container = ev.DropObject.GetComponentInChildren<StorageContainer>();
                        if (container)
                        {
                            Network.Identifier.SetIdentityId(container.gameObject, lifePod.StorageUniqueId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"OnLifepodInterpolation: {ex}");
            }
        }

        public static void InitializeStorage(Subnautica.Network.Models.Storage.World.Childrens.SupplyDrop supplyDrop)
        {
            var storageContainer = Network.Identifier.GetComponentByGameObject<global::StorageContainer>(supplyDrop.StorageUniqueId);
            if (storageContainer)
            {
                foreach (var item in supplyDrop.StorageContainer.Items)
                {
                    Entity.SpawnToQueue(item.Item, item.ItemId, storageContainer.container);
                }
            }
        }

        public static void ForceSupplyDrop(string key)
        {
            foreach (var dropData in SupplyDropManager.allDropData)
            {
                if (dropData != null && !SupplyDropManager.main.completedDropKeys.Contains(dropData.GetKey()) && StoryGoal.Equals(dropData.precondition, key))
                {
                    SupplyDropManager.main.PerformDrop(dropData);
                }
            }
        }

        public static void OnEntitySpawning(EntitySpawningEventArgs ev)
        {
            if (ev.ClassId == API.Constants.SupplyDrop.LifepodFabricatorClassId)
            {
                ev.UniqueId = Network.Session.Current.SupplyDrops.First().FabricatorUniqueId;
            }
        }

        public static void OnStorageItemAdding(StorageItemAddingEventArgs ev)
        {
            if (ev.TechType == TechType.EscapePod)
            {
                ev.IsAllowed = false;

                LifepodProcessor.SendPacketToServer(ev.UniqueId, pickupItem: WorldPickupItem.Create(ev.Item, PickupSourceType.PlayerInventory), isAdded: true);
            }
        }

        public static void OnStorageItemRemoving(StorageItemRemovingEventArgs ev)
        {
            if (ev.TechType == TechType.EscapePod)
            {
                ev.IsAllowed = false;

                LifepodProcessor.SendPacketToServer(ev.UniqueId, pickupItem: WorldPickupItem.Create(ev.Item, PickupSourceType.StorageContainer));
            }
        }

        public static void SendPacketToServer(string uniqueId, WorldPickupItem pickupItem = null, bool isAdded = false)
        {
            ServerModel.LifepodArgs request = new ServerModel.LifepodArgs()
            {
                UniqueId = uniqueId,
                IsAdded = isAdded,
                WorldPickupItem = pickupItem,
            };

            NetworkClient.SendPacket(request);
        }
    }
}