namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using Subnautica.API.Enums;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Helper;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Metadata;
    using Subnautica.Network.Models.Server;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using UnityEngine;
    using Metadata = Subnautica.Network.Models.Metadata;
    using ServerModel = Subnautica.Network.Models.Server;
    using Subnautica.API.Extensions;

    public class FridgeProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(
      string uniqueId,
      TechType techType,
      MetadataComponentArgs packet,
      bool isSilence)
        {
            Subnautica.Network.Models.Metadata.Fridge component = packet.Component.GetComponent<Subnautica.Network.Models.Metadata.Fridge>();
            if (component == null)
                return false;
            if (isSilence)
            {
                if (component.StorageContainer == null)
                    return false;
                global::Fridge componentByGameObject = Subnautica.API.Features.Network.Identifier.GetComponentByGameObject<global::Fridge>(uniqueId);
                if (componentByGameObject == null)
                    return false;
                foreach (StorageItem storageItem in component.StorageContainer.Items)
                {
                    StorageItem item = storageItem;
                    FridgeItemComponent fridgeItemComponent = component.Components.FirstOrDefault<FridgeItemComponent>((Func<FridgeItemComponent, bool>)(q => q.ItemId == item.ItemId));
                    if (fridgeItemComponent != null)
                    {
                        ItemQueueAction action = new ItemQueueAction();
                        action.OnEntitySpawned = new Action<ItemQueueProcess, Pickupable, GameObject>(this.OnEntitySpawned);
                        action.RegisterProperty("CustomProperty", (object)new Subnautica.Network.Models.Metadata.Fridge()
                        {
                            ItemComponent = fridgeItemComponent
                        });
                        Entity.SpawnToQueue(item.Item, item.ItemId, componentByGameObject.storageContainer.container, action);
                    }
                }
                return true;
            }
            if (component.IsPowerStateChanged)
            {
                ItemQueueAction action = new ItemQueueAction();
                action.OnProcessCompleted = new Action<ItemQueueProcess>(this.OnProcessCompleted);
                action.RegisterProperty("UniqueId", (object)uniqueId);
                action.RegisterProperty("Fridge", (object)component);
                Entity.ProcessToQueue(action);
            }
            else if (component.IsAdded)
                Subnautica.API.Features.Network.Storage.AddItemToStorage(uniqueId, packet.GetPacketOwnerId(), component.WorldPickupItem, new Action<ItemQueueProcess, Pickupable, GameObject>(this.OnEntitySpawned), (object)component);
            else
                Subnautica.API.Features.Network.Storage.AddItemToInventory(packet.GetPacketOwnerId(), component.WorldPickupItem, new Action<ItemQueueProcess, Pickupable, GameObject>(this.OnEntitySpawned), (object)component);
            return true;
        }

        public void OnProcessCompleted(ItemQueueProcess item)
        {
            global::Fridge componentByGameObject = Network.Identifier.GetComponentByGameObject<global::Fridge>(item.Action.GetProperty<string>("UniqueId"));
            if (!componentByGameObject)
                return;
            Subnautica.Network.Models.Metadata.Fridge property = item.Action.GetProperty<Subnautica.Network.Models.Metadata.Fridge>("Fridge");
            foreach (InventoryItem inventoryItem1 in (IEnumerable<InventoryItem>)componentByGameObject.storageContainer.container)
            {
                InventoryItem inventoryItem = inventoryItem1;
                Eatable eatable;
                if (((Component)inventoryItem.item).TryGetComponent<Eatable>(out eatable) && eatable.decomposes)
                {
                    FridgeItemComponent fridgeItemComponent = property.Components.FirstOrDefault<FridgeItemComponent>((Func<FridgeItemComponent, bool>)(q => q.ItemId == ((Component)inventoryItem.item).gameObject.GetIdentityId()));
                    if (fridgeItemComponent != null)
                    {
                        eatable.decayPaused = fridgeItemComponent.IsPaused;
                        eatable.timeDecayPause = fridgeItemComponent.TimeDecayPause;
                        eatable.timeDecayStart = fridgeItemComponent.TimeDecayStart;
                    }
                }
            }
        }

        public void OnEntitySpawned(
          ItemQueueProcess item,
          Pickupable pickupable,
          GameObject gameObject)
        {
            Fridge property = item.Action.GetProperty<Fridge>("CustomProperty");
            Eatable eatable;
            if (property.ItemComponent == null || !property.ItemComponent.IsDecomposes || !((Component)pickupable).TryGetComponent<Eatable>(out eatable))
                return;
            eatable.decayPaused = property.ItemComponent.IsPaused;
            eatable.timeDecayPause = property.ItemComponent.TimeDecayPause;
            eatable.timeDecayStart = property.ItemComponent.TimeDecayStart;
        }

        public static void OnStorageItemAdding(StorageItemAddingEventArgs ev)
        {
            if (ev.TechType != TechType.Fridge)
                return;
            ev.IsAllowed = false;
            Eatable eatable;
            if (((Component)ev.Item).TryGetComponent<Eatable>(out eatable))
                FridgeProcessor.SendPacketToServer(ev.UniqueId, WorldPickupItem.Create(ev.Item, PickupSourceType.PlayerInventory), eatable.decomposes, eatable.timeDecayStart, true);
            else
                FridgeProcessor.SendPacketToServer(ev.UniqueId, WorldPickupItem.Create(ev.Item, PickupSourceType.PlayerInventory), isAdded: true);
        }

        public static void OnStorageItemRemoving(StorageItemRemovingEventArgs ev)
        {
            if (ev.TechType != TechType.Fridge)
                return;
            ev.IsAllowed = false;
            FridgeProcessor.SendPacketToServer(ev.UniqueId, WorldPickupItem.Create(ev.Item, PickupSourceType.StorageContainer));
        }

        public static void SendPacketToServer(
          string uniqueId,
          WorldPickupItem pickupItem = null,
          bool isDecomposes = false,
          float timeDecayStart = 0.0f,
          bool isAdded = false)
        {
            NetworkClient.SendPacket(new MetadataComponentArgs()
            {
                UniqueId = uniqueId,
                Component = new Fridge()
                {
                    IsAdded = isAdded,
                    WorldPickupItem = pickupItem,
                    IsDecomposes = isDecomposes,
                    TimeDecayStart = timeDecayStart
                }
            });
        }
    }
}
