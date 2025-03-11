namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Helper;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Client.Extensions;
    using Subnautica.Client.MonoBehaviours.World;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Metadata;
    using Subnautica.Network.Models.Server;
    using System.Collections.Generic;
    using UnityEngine;

    using Metadata = Subnautica.Network.Models.Metadata;
    using ServerModel = Subnautica.Network.Models.Server;

    public class PlanterProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(
      string uniqueId,
      TechType techType,
      MetadataComponentArgs packet,
      bool isSilence)
        {
            Subnautica.Network.Models.Metadata.Planter component = packet.Component.GetComponent<Subnautica.Network.Models.Metadata.Planter>();
            if (component == null)
                return false;
            global::Planter planter = this.GetPlanter(packet.UniqueId, component);
            if (planter == null)
                return false;
            if (isSilence)
            {
                foreach (PlanterItem planterItem in component.Items)
                    Entity.SpawnToQueue(planterItem.TechType, planterItem.ItemId, planter.storageContainer.container, this.GetItemAction(planter, planterItem, 1));
                return true;
            }
            if (component.CurrentItem != null)
            {
                if (component.IsAdding)
                {
                    if (ZeroPlayer.IsPlayerMine(packet.GetPacketOwnerId()))
                        Entity.ProcessToQueue(this.GetItemAction(planter, component.CurrentItem, 0, true));
                    else
                        Entity.SpawnToQueue(component.CurrentItem.TechType, component.CurrentItem.ItemId, planter.storageContainer.container, this.GetItemAction(planter, component.CurrentItem, 1));
                }
                else if (component.IsHarvesting)
                {
                    if (ZeroPlayer.IsPlayerMine(packet.GetPacketOwnerId()))
                        Entity.ProcessToQueue(this.GetItemAction(planter, component.CurrentItem, 3, true));
                    else
                        Entity.ProcessToQueue(this.GetItemAction(planter, component.CurrentItem, 3));
                }
                else if ((double)component.CurrentItem.Health != -1.0)
                {
                    if ((double)component.CurrentItem.Health <= 0.0)
                        Entity.RemoveToQueue(component.CurrentItem.ItemId);
                    else
                        Entity.ProcessToQueue(this.GetItemAction(planter, component.CurrentItem, 2));
                }
            }
            return true;
        }

        private global::Planter GetPlanter(string uniqueId, Subnautica.Network.Models.Metadata.Planter component)
        {
            BaseDeconstructable componentByGameObject = Subnautica.API.Features.Network.Identifier.GetComponentByGameObject<BaseDeconstructable>(uniqueId, true);
            WaterPark baseWaterPark = componentByGameObject != null ? componentByGameObject.GetBaseWaterPark() : (WaterPark)null;
            if (baseWaterPark == null)
                return Network.Identifier.GetComponentByGameObject<global::Planter>(uniqueId);
            if (!(baseWaterPark is LargeRoomWaterPark largeRoomWaterPark))
                return baseWaterPark.planter;
            return component.IsLeft ? largeRoomWaterPark.planters.leftPlanter : largeRoomWaterPark.planters.rightPlanter;
        }

        private ItemQueueAction GetItemAction(
          global::Planter planter,
          PlanterItem item,
          int processType,
          bool isMine = false)
        {
            ItemQueueAction itemAction = new ItemQueueAction();
            itemAction.RegisterProperty("Item", (object)item);
            itemAction.RegisterProperty("IsMine", (object)isMine);
            itemAction.RegisterProperty("Planter", (object)planter);
            switch (processType)
            {
                case 0:
                    itemAction.OnProcessCompleted = this.OnItemProcessCompleted;
                    break;
                case 1:
                    itemAction.OnEntitySpawned = this.OnEntitySpawned;
                    break;
                case 2:
                    itemAction.OnProcessCompleted = this.OnHealthProcessCompleted;
                    break;
                case 3:
                    itemAction.OnProcessCompleted = this.OnHarvestingProcessCompleted;
                    break;
            }
            return itemAction;
        }

        public void OnItemProcessCompleted(ItemQueueProcess item)
        {
            PlanterItem property1 = item.Action.GetProperty<PlanterItem>("Item");
            global::Planter property2 = item.Action.GetProperty<global::Planter>("Planter");
            if (property1 == null || property2 == null)
                return;
            global::Planter.PlantSlot slotById = property2.GetSlotByID((int)property1.SlotId);
            if (slotById != null && slotById.isOccupied)
            {
                PlanterItemComponent planterItemComponent = Radical.EnsureComponent<PlanterItemComponent>(((Component)slotById.plantable).gameObject);
                planterItemComponent.SetHealth(property1.Health);
                planterItemComponent.SetTimeNextFruit(property1.TimeNextFruit);
                planterItemComponent.SetActiveFruitCount(property1.ActiveFruitCount);
                planterItemComponent.SetStartingTime(property1.TimeStartGrowth);
                if (slotById.plantable && slotById.plantable.growingPlant)
                    slotById.plantable.growingPlant.timeStartGrowth = property1.TimeStartGrowth;
            }
        }

        public void OnEntitySpawned(
          ItemQueueProcess item,
          Pickupable pickupable,
          GameObject gameObject)
        {
            PlanterItem property1 = item.Action.GetProperty<PlanterItem>("Item");
            global::Planter property2 = item.Action.GetProperty<global::Planter>("Planter");
            if (property1 == null || property2 == null)
                return;
            Plantable component = ((Component)pickupable.inventoryItem.item).GetComponent<Plantable>();
            pickupable.inventoryItem.item.SetTechTypeOverride(component.plantTechType);
            pickupable.inventoryItem.isEnabled = false;
            using (EventBlocker.Create(TechType.PlanterPot))
            {
                using (EventBlocker.Create(TechType.PlanterPot2))
                {
                    using (EventBlocker.Create(TechType.PlanterPot3))
                    {
                        using (EventBlocker.Create(TechType.PlanterBox))
                        {
                            using (EventBlocker.Create(TechType.PlanterShelf))
                            {
                                using (EventBlocker.Create(TechType.FarmingTray))
                                {
                                    using (EventBlocker.Create(TechType.BaseWaterPark))
                                        property2.AddItem(component, (int)property1.SlotId);
                                }
                            }
                        }
                    }
                }
            }
            if (component != null && component.gameObject != null)
            {
                PlanterItemComponent planterItemComponent = Radical.EnsureComponent<PlanterItemComponent>(((Component)component).gameObject);
                planterItemComponent.SetHealth(property1.Health);
                planterItemComponent.SetTimeNextFruit(property1.TimeNextFruit);
                planterItemComponent.SetActiveFruitCount(property1.ActiveFruitCount);
                planterItemComponent.SetStartingTime(property1.TimeStartGrowth);
            }
            global::Planter.PlantSlot slotById = property2.GetSlotByID((int)property1.SlotId);
            if (slotById != null && slotById.isOccupied && slotById.plantable && slotById.plantable.growingPlant)
                slotById.plantable.growingPlant.timeStartGrowth = property1.TimeStartGrowth;
        }

        public void OnHealthProcessCompleted(ItemQueueProcess item)
        {
            PlanterItem property = item.Action.GetProperty<PlanterItem>("Item");
            if (property == null)
                return;
            Plantable componentByGameObject = Subnautica.API.Features.Network.Identifier.GetComponentByGameObject<Plantable>(property.ItemId);
            if (componentByGameObject)
                Radical.EnsureComponent<PlanterItemComponent>(((Component)componentByGameObject).gameObject).SetHealth(property.Health);
        }

        public static void OnPlanterProgressCompleted(PlanterProgressCompletedEventArgs ev)
        {
            PlanterItemComponent planterItemComponent;
            LiveMixin liveMixin;
            if (!((Component)ev.Plantable).TryGetComponent<PlanterItemComponent>(out planterItemComponent) || (double)planterItemComponent.Health <= 0.0 || !ev.GrownPlant.TryGetComponent<LiveMixin>(out liveMixin))
                return;
            liveMixin.health = planterItemComponent.Health;
        }

        public void OnHarvestingProcessCompleted(ItemQueueProcess item)
        {
            PlanterItem property = item.Action.GetProperty<PlanterItem>("Item");
            if (property == null)
                return;
            Plantable componentByGameObject = Subnautica.API.Features.Network.Identifier.GetComponentByGameObject<Plantable>(property.ItemId);
            if (componentByGameObject)
            {
                PlanterItemComponent planterItemComponent = Radical.EnsureComponent<PlanterItemComponent>(((Component)componentByGameObject).gameObject);
                planterItemComponent.SetTimeNextFruit(property.TimeNextFruit);
                planterItemComponent.SetActiveFruitCount(property.ActiveFruitCount);
                FruitPlant fruitPlant;
                if (componentByGameObject.linkedGrownPlant && ((Component)componentByGameObject.linkedGrownPlant).TryGetComponent<FruitPlant>(out fruitPlant))
                {
                    fruitPlant.inactiveFruits.Clear();
                    fruitPlant.timeNextFruit = property.TimeNextFruit;
                    foreach (PickPrefab fruit in fruitPlant.fruits)
                    {
                        if (property.ActiveFruitCount > (byte)0)
                        {
                            --property.ActiveFruitCount;
                            if (fruit.pickedState)
                                fruit.SetPickedState(false);
                        }
                        else if (!fruit.pickedState)
                            fruit.SetPickedState(true);
                        if (fruit.pickedState)
                            fruitPlant.inactiveFruits.Add(fruit);
                    }
                    if (item.Action.GetProperty<bool>("IsMine"))
                        CraftData.AddToInventory(fruitPlant.fruits[0].pickTech, spawnIfCantAdd: false);
                }
                else if (componentByGameObject.linkedGrownPlant)
                {
                    componentByGameObject.linkedGrownPlant.seed.currentPlanter.RemoveItem(componentByGameObject.linkedGrownPlant.seed);
                    PickPrefab pickPrefab;
                    if (((Component)componentByGameObject.linkedGrownPlant).TryGetComponent<PickPrefab>(out pickPrefab))
                    {
                        if (item.Action.GetProperty<bool>("IsMine"))
                            CraftData.AddToInventory(pickPrefab.pickTech, spawnIfCantAdd: false);
                        World.DestroyGameObject(((Component)componentByGameObject).gameObject);
                    }
                    else if (item.Action.GetProperty<bool>("IsMine"))
                    {
                        global::Inventory.Get().Pickup(componentByGameObject.linkedGrownPlant.seed.pickupable);
                        global::Player.main.PlayGrab();
                    }
                    else
                        World.DestroyGameObject(((Component)componentByGameObject).gameObject);
                }
            }
        }

        public static void OnPlanterGrowned(PlanterGrownedEventArgs ev)
        {
            GrownPlant grownPlant;
            PlanterItemComponent planterItemComponent;
            if (!((Component)ev.FruitPlant).TryGetComponent<GrownPlant>(out grownPlant) || !((Component)grownPlant.seed).TryGetComponent<PlanterItemComponent>(out planterItemComponent))
                return;
            ev.FruitPlant.timeNextFruit = planterItemComponent.TimeNextFruit;
            for (int index = 0; index < (int)planterItemComponent.ActiveFruitCount; ++index)
                ev.FruitPlant.fruits[index].SetPickedState(false);
        }

        public static void OnTakeDamaging(TakeDamagingEventArgs ev)
        {
            if (ev.IsStaticWorldEntity || !ev.IsDestroyable || (double)ev.Damage <= 0.0)
                return;
            global::Planter componentInParent1 = ((Component)ev.LiveMixin).GetComponentInParent<global::Planter>();
            if (componentInParent1 == null)
                return;
            KeyValuePair<string, KeyValuePair<TechType, bool>> detail = componentInParent1.GetDetail();
            if (detail.Key.IsNull())
                return;
            GrownPlant componentInParent2 = ((Component)ev.LiveMixin).GetComponentInParent<GrownPlant>();
            if (componentInParent2 && componentInParent2.seed)
                PlanterProcessor.SendPacketToServer(detail.Key, ((Component)componentInParent2.seed).gameObject.GetIdentityId(), isLeft: detail.Value.Value, health: ev.NewHealth);
        }

        public static void OnFruitHarvesting(FruitHarvestingEventArgs ev)
        {
            if (ev.IsStaticWorldEntity)
                return;
            global::Planter componentInParent1 = ((Component)ev.PickPrefab).GetComponentInParent<global::Planter>();
            if (componentInParent1)
            {
                KeyValuePair<string, KeyValuePair<TechType, bool>> detail = componentInParent1.GetDetail();
                if (detail.Key.IsNotNull())
                {
                    GrownPlant componentInParent2 = ((Component)ev.PickPrefab).GetComponentInParent<GrownPlant>();
                    if (componentInParent2 && componentInParent2.seed)
                    {
                        ev.IsAllowed = false;
                        PlanterProcessor.SendPacketToServer(detail.Key, ((Component)componentInParent2.seed).gameObject.GetIdentityId(), ev.MaxSpawnableFruit, ev.SpawnInterval, isLeft: detail.Value.Value, isHarvesting: true);
                    }
                }
            }
        }

        public static void OnGrownPlantHarvesting(GrownPlantHarvestingEventArgs ev)
        {
            global::Planter componentInParent = ((Component)ev.GrownPlant).GetComponentInParent<global::Planter>();
            if (componentInParent != null)
                return;
            ev.IsAllowed = false;
            KeyValuePair<string, KeyValuePair<TechType, bool>> detail = componentInParent.GetDetail();
            if (detail.Key.IsNotNull())
                PlanterProcessor.SendPacketToServer(detail.Key, ev.UniqueId, isLeft: detail.Value.Value, isHarvesting: true);
        }

        public static void OnPlanterItemAdded(PlanterItemAddedEventArgs ev)
        {
            LiveMixin liveMixin;
            if (((Component)ev.Plantable).TryGetComponent<LiveMixin>(out liveMixin))
            {
                string uniqueId = ev.UniqueId;
                Plantable plantable = ev.Plantable;
                string itemId = ev.ItemId;
                Plantable item = plantable;
                int slotId = ev.SlotId;
                int num = ev.IsLeft ? 1 : 0;
                double health = (double)liveMixin.health == -1.0 ? (double)liveMixin.maxHealth : (double)liveMixin.health;
                PlanterProcessor.SendPacketToServer(uniqueId, itemId, item: item, slotId: slotId, isLeft: num != 0, health: (float)health, isAdding: true);
            }
            else
            {
                string uniqueId = ev.UniqueId;
                Plantable plantable = ev.Plantable;
                string itemId = ev.ItemId;
                Plantable item = plantable;
                int slotId = ev.SlotId;
                int num = ev.IsLeft ? 1 : 0;
                PlanterProcessor.SendPacketToServer(uniqueId, itemId, item: item, slotId: slotId, isLeft: num != 0, isAdding: true);
            }
        }

        private static void SendPacketToServer(
          string uniqueId,
          string itemId = null,
          byte maxSpawnableFruit = 0,
          float fruitSpawnInterval = -1f,
          Plantable item = null,
          int slotId = -1,
          bool isLeft = false,
          float health = -1f,
          bool isHarvesting = false,
          bool isAdding = false)
        {
            PlanterItem planterItem = new PlanterItem();
            planterItem.ItemId = itemId;
            planterItem.SlotId = (byte)slotId;
            planterItem.Health = health;
            planterItem.MaxSpawnableFruit = maxSpawnableFruit;
            planterItem.FruitSpawnInterval = fruitSpawnInterval;
            if (item == null)
            {
                planterItem.TechType = CraftData.GetTechType(((Component)item.pickupable).gameObject);
                if (item.growingPlant)
                    planterItem.Duration = (short)item.growingPlant.growthDuration;
            }
            NetworkClient.SendPacket((NetworkPacket)new MetadataComponentArgs()
            {
                UniqueId = uniqueId,
                Component = (MetadataComponent)new Subnautica.Network.Models.Metadata.Planter()
                {
                    IsHarvesting = isHarvesting,
                    IsAdding = isAdding,
                    IsLeft = isLeft,
                    CurrentItem = (itemId == null ? (PlanterItem)null : planterItem)
                }
            });
        }
    }
}
