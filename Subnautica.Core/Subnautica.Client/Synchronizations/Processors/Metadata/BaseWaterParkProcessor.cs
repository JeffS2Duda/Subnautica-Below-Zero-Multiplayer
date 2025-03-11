namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Helper;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Client.Extensions;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Metadata;
    using Subnautica.Network.Models.Server;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;
    using System;
    using UnityEngine;

    public class BaseWaterParkProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(
          string uniqueId,
          TechType techType,
          MetadataComponentArgs packet,
          bool isSilence)
        {
            BaseWaterPark component = packet.Component.GetComponent<BaseWaterPark>();
            if (component == null)
                return false;
            if (isSilence)
            {
                if (component.LeftPlanter != null)
                    MetadataProcessor.ExecuteProcessor(TechType.FarmingTray, uniqueId, component.LeftPlanter, true);
                if (component.RightPlanter != null)
                    MetadataProcessor.ExecuteProcessor(TechType.FarmingTray, uniqueId, component.RightPlanter, true);
                return true;
            }
            if (component.ProcessType == BaseWaterParkProcessType.Full)
                ErrorMessage.AddMessage("WaterPark Full!");
            else if (component.ProcessType == BaseWaterParkProcessType.ItemDrop)
            {
                if (ZeroPlayer.IsPlayerMine(packet.GetPacketOwnerId()))
                    World.DestroyItemFromPlayer(component.Entity.UniqueId, true);
                if (component.WorldPickupItem.Item.TechType.IsCreatureEgg())
                    Network.DynamicEntity.Spawn(component.Entity, new Action<ItemQueueProcess, Pickupable, GameObject>(this.OnEntitySpawned), uniqueId);
            }
            else if (component.ProcessType == BaseWaterParkProcessType.ItemPickup)
            {
                if (component.WorldPickupItem.Item.ItemId.IsMultiplayerCreature())
                {
                    if (ZeroPlayer.IsPlayerMine(packet.GetPacketOwnerId()))
                        Entity.SpawnToQueue(component.WorldPickupItem.Item.Item, component.WorldPickupItem.GetItemId(), global::Inventory.main.container);
                }
                else if (ZeroPlayer.IsPlayerMine(packet.GetPacketOwnerId()))
                {
                    ItemQueueAction action = new ItemQueueAction();
                    action.OnProcessCompleted = new Action<ItemQueueProcess>(this.OnPickupProcessCompleted);
                    action.RegisterProperty("ItemId", component.WorldPickupItem.Item.ItemId);
                    Entity.ProcessToQueue(action);
                }
                else
                    Network.DynamicEntity.Remove(component.WorldPickupItem.GetItemId());
            }
            else if (component.ProcessType == BaseWaterParkProcessType.Spawn)
            {
                foreach (WorldDynamicEntity creature in component.Creatures)
                {
                    Network.DataStorage.SetProperty(creature.Id.ToCreatureStringId(), "WaterParkId", uniqueId);
                    Network.DataStorage.SetProperty(creature.Id.ToCreatureStringId(), "AddedTime", creature.AddedTime);
                    if (creature.UniqueId.IsNotNull())
                        Network.DynamicEntity.Remove(creature.UniqueId);
                }
            }
            else if (component.ProcessType == BaseWaterParkProcessType.WaterParkChange)
            {
                foreach (WorldDynamicEntity creature in component.Creatures)
                {
                    if (creature.Id > (ushort)0)
                        Network.DataStorage.SetProperty(creature.Id.ToCreatureStringId(), "WaterParkId", creature.ParentId);
                    else if (creature.ParentId.IsNotNull())
                    {
                        ItemQueueAction action = new ItemQueueAction();
                        action.OnProcessCompleted = new Action<ItemQueueProcess>(this.OnCreatureEggWaterParkChanged);
                        action.RegisterProperty("EggId", (object)creature.UniqueId);
                        action.RegisterProperty("WaterParkId", (object)creature.ParentId);
                        Entity.ProcessToQueue(action);
                    }
                    else
                        Network.DynamicEntity.Remove(creature.UniqueId);
                }
            }
            return true;
        }

        public void OnCreatureEggWaterParkChanged(ItemQueueProcess item)
        {
            string property1 = item.Action.GetProperty<string>("EggId");
            string property2 = item.Action.GetProperty<string>("WaterParkId");
            if (!property2.IsNotNull())
                return;
            WaterParkItem componentByGameObject1 = Network.Identifier.GetComponentByGameObject<WaterParkItem>(property1);
            BaseDeconstructable componentByGameObject2 = Network.Identifier.GetComponentByGameObject<BaseDeconstructable>(property2);
            WaterPark baseWaterPark = componentByGameObject2 != null ? componentByGameObject2.GetBaseWaterPark() : null;
            if (baseWaterPark && componentByGameObject1)
            {
                componentByGameObject1.SetWaterPark(baseWaterPark);
                componentByGameObject1.ValidatePosition();
            }
        }

        public void OnPickupProcessCompleted(ItemQueueProcess item)
        {
            string property = item.Action.GetProperty<string>("ItemId");
            if (!property.IsNotNull())
                return;
            Network.DynamicEntity.RemoveEntity(property);
            Pickupable componentByGameObject = Network.Identifier.GetComponentByGameObject<Pickupable>(property);
            if (componentByGameObject)
                componentByGameObject.LocalPickup();
        }

        public void OnEntitySpawned(
          ItemQueueProcess item,
          Pickupable pickupable,
          GameObject gameObject)
        {
            string property1 = item.Action.GetProperty<string>("CustomProperty");
            WorldDynamicEntity property2 = item.Action.GetProperty<WorldDynamicEntity>("Entity");
            if (property2 == null)
                return;
            Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.WaterParkCreature component = property2.Component.GetComponent<Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.WaterParkCreature>();
            if (component != null)
                pickupable.MultiplayerDrop(true, waterParkId: property1, waterParkAddTime: component.AddedTime);
        }

        public static void OnPlayerItemDroping(PlayerItemDropingEventArgs ev)
        {
            if (ev.WaterParkId.IsNotNull())
            {
                ev.IsAllowed = false;
                if (ev.TechType.ToCreatureEgg().IsSynchronizedCreature())
                    BaseWaterParkProcessor.SendPacketToServer(ev.WaterParkId, WorldPickupItem.Create(ev.Item, PickupSourceType.PlayerInventoryDrop), ev.Position.ToZeroVector3(), ev.Rotation.ToZeroQuaternion(), ev.Item.ToWaterParkCreatureComponent(), BaseWaterParkProcessType.ItemDrop);
                else
                    ErrorMessage.AddMessage("Not synchronised yet. Wait for the next update. [" + ev.TechType.ToString() + "]");
            }
            else
            {
                if (!ev.TechType.IsCreature() || !ev.TechType.IsSynchronizedCreature())
                    return;
                ev.IsAllowed = false;
            }
        }

        public static void OnPlayerItemPickedUp(PlayerItemPickedUpEventArgs ev)
        {
            if (!ev.WaterParkId.IsNotNull() && !ev.ItemWaterParkId.IsNotNull() || !ev.TechType.ToCreatureEgg().IsSynchronizedCreature())
                return;
            ev.IsAllowed = false;
            BaseWaterParkProcessor.SendPacketToServer(ev.WaterParkId, WorldPickupItem.Create(ev.Pickupable), processType: BaseWaterParkProcessType.ItemPickup);
        }

        public static void SendPacketToServer(
          string uniqueId,
          WorldPickupItem pickupItem,
          ZeroVector3 position = null,
          ZeroQuaternion rotation = null,
          Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.WaterParkCreature waterParkCreature = null,
          BaseWaterParkProcessType processType = BaseWaterParkProcessType.None)
        {
            NetworkClient.SendPacket(new MetadataComponentArgs()
            {
                UniqueId = uniqueId,
                Component = new BaseWaterPark()
                {
                    ProcessType = processType,
                    WorldPickupItem = pickupItem,
                    WaterParkCreature = waterParkCreature,
                    Entity = new WorldDynamicEntity()
                    {
                        Position = position,
                        Rotation = rotation
                    }
                }
            });
        }

    }
}
