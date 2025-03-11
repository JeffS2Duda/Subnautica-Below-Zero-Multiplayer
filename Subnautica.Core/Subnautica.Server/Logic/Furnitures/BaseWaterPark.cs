namespace Subnautica.Server.Logic.Furnitures;

using Subnautica.API.Enums;
using Subnautica.API.Extensions;
using Subnautica.API.Features;
using Subnautica.Events.EventArgs;
using Subnautica.Network.Core.Components;
using Subnautica.Network.Models.Core;
using Subnautica.Network.Models.Creatures;
using Subnautica.Network.Models.Metadata;
using Subnautica.Network.Models.Server;
using Subnautica.Network.Models.Storage.Construction;
using Subnautica.Network.Models.Storage.World.Childrens;
using Subnautica.Network.Structures;
using Subnautica.Server.Abstracts;
using Subnautica.Server.Core;
using Subnautica.Server.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseWaterPark : BaseLogic
{
    private Queue<DeconstructionBeginArgs> DeconstructionQueue = new Queue<DeconstructionBeginArgs>();
    private Subnautica.Network.Models.Metadata.BaseWaterPark WaterParkChangePacket = new Subnautica.Network.Models.Metadata.BaseWaterPark();

    public StopwatchItem Timing { get; set; } = new StopwatchItem(5000f);

    public Dictionary<ushort, BaseWaterParkCreatureItem> CreatureWaterParks { get; set; } = new Dictionary<ushort, BaseWaterParkCreatureItem>();

    public Dictionary<string, HashSet<ushort>> Requests { get; set; } = new Dictionary<string, HashSet<ushort>>();

    public bool IsCreatureRegistered { get; set; } = false;

    public override void OnUnscaledFixedUpdate(float fixedDeltaTime)
    {
        if (this.IsCreatureRegistered || !Subnautica.Server.Core.Server.Instance.Logices.CreatureWatcher.IsLoaded())
            return;
        this.IsCreatureRegistered = true;
        foreach (KeyValuePair<string, ConstructionItem> baseWaterPark1 in this.GetBaseWaterParks())
        {
            Subnautica.Network.Models.Metadata.BaseWaterPark baseWaterPark2 = baseWaterPark1.Value.EnsureComponent<Subnautica.Network.Models.Metadata.BaseWaterPark>();
            if (baseWaterPark2 != null && baseWaterPark2.Creatures.Count > 0)
            {
                foreach (WorldDynamicEntity creature in baseWaterPark2.Creatures)
                    this.RegisterCreature(baseWaterPark1.Value.UniqueId, creature);
            }
        }
    }

    public override void OnFixedUpdate(float fixedDeltaTime)
    {
        if (this.Timing.IsFinished() && Subnautica.API.Features.World.IsLoaded)
        {
            this.Timing.Restart();
            double serverTimeAsDouble = Subnautica.Server.Core.Server.Instance.Logices.World.GetServerTimeAsDouble();
            foreach (KeyValuePair<string, ConstructionItem> baseWaterPark1 in this.GetBaseWaterParks())
            {
                Subnautica.Network.Models.Metadata.BaseWaterPark baseWaterPark2 = baseWaterPark1.Value.EnsureComponent<Subnautica.Network.Models.Metadata.BaseWaterPark>();
                if (baseWaterPark2 != null && baseWaterPark2.Eggs.Count > 0)
                {
                    foreach (string uniqueId in baseWaterPark2.Eggs.ToList<string>())
                    {
                        WorldDynamicEntity dynamicEntity = Subnautica.Server.Core.Server.Instance.Storages.World.GetDynamicEntity(uniqueId);
                        if (dynamicEntity != null)
                        {
                            Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.WaterParkCreature component = dynamicEntity.Component.GetComponent<Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.WaterParkCreature>();
                            if (component != null && component.IsSpawnable(serverTimeAsDouble))
                            {
                                Subnautica.Server.Core.Server.Instance.Storages.World.RemoveDynamicEntity(uniqueId);
                                if (baseWaterPark2.AddCreature(dynamicEntity))
                                {
                                    baseWaterPark2.RemoveCreatureEgg(uniqueId);
                                    component.SpawnChildren(serverTimeAsDouble);
                                    this.RegisterCreature(baseWaterPark1.Value.UniqueId, dynamicEntity, true);
                                }
                            }
                        }
                    }
                }
            }
            this.SendPackets();
        }
        while (this.DeconstructionQueue.Count > 0)
            this.ConsumeDeconstruction(this.DeconstructionQueue.Dequeue());
    }

    public void OnPlanterStorageReseting(PlanterStorageResetingEventArgs ev)
    {
        if (!Subnautica.API.Features.World.IsLoaded)
            return;
        Subnautica.Network.Models.Metadata.BaseWaterPark waterPark = this.GetWaterPark(ev.UniqueId);
        if (waterPark != null)
        {
            if (ev.IsLeft)
                waterPark.LeftPlanter.Items.Clear();
            else
                waterPark.RightPlanter.Items.Clear();
        }
    }

    public void OnCreatureDead(MultiplayerCreatureItem creature)
    {
        BaseWaterParkCreatureItem parkCreatureItem;
        if (!this.CreatureWaterParks.TryGetValue(creature.Id, out parkCreatureItem))
            return;
        this.CreatureWaterParks.Remove(creature.Id);
        foreach (KeyValuePair<string, ConstructionItem> baseWaterPark1 in Subnautica.Server.Core.Server.Instance.Logices.BaseWaterPark.GetBaseWaterParks())
        {
            Subnautica.Network.Models.Metadata.BaseWaterPark baseWaterPark2 = baseWaterPark1.Value.EnsureComponent<Subnautica.Network.Models.Metadata.BaseWaterPark>();
            if (baseWaterPark2 != null && baseWaterPark2.GetCreature(parkCreatureItem.EntityId) != null)
                baseWaterPark2.RemoveCreature(parkCreatureItem.EntityId);
        }
    }

    public void OnDestroyableDynamicEntityDestroyed(string uniqueId)
    {
        foreach (KeyValuePair<string, ConstructionItem> baseWaterPark in this.GetBaseWaterParks())
            baseWaterPark.Value.EnsureComponent<Subnautica.Network.Models.Metadata.BaseWaterPark>()?.RemoveCreatureEgg(uniqueId);
    }

    public bool OnItemPickup(AuthorizationProfile profile, Subnautica.Network.Models.Metadata.BaseWaterPark component)
    {
        if (component.WorldPickupItem.Item.TechType.IsCreature())
        {
            string itemId = component.WorldPickupItem.GetItemId();
            if (!itemId.IsMultiplayerCreature())
                return false;
            ushort creatureId = itemId.ToCreatureId();
            if (creatureId <= (ushort)0)
                return false;
            KeyValuePair<ushort, BaseWaterParkCreatureItem> keyValuePair = this.CreatureWaterParks.FirstOrDefault<KeyValuePair<ushort, BaseWaterParkCreatureItem>>((Func<KeyValuePair<ushort, BaseWaterParkCreatureItem>, bool>)(q => (int)q.Key == (int)creatureId));
            if (keyValuePair.Value == null)
                return false;
            component.WorldPickupItem.SetCustomId(keyValuePair.Value.EntityId);
            component.WorldPickupItem.SetSource(PickupSourceType.NoSource);
            if (!profile.InventoryItems.IsCanBeAdded(component.WorldPickupItem))
                return false;
            this.CreatureWaterParks.Remove(creatureId);
            Subnautica.Server.Core.Server.Instance.Logices.CreatureWatcher.UnRegisterCreature(creatureId);
        }
        string itemId1 = component.WorldPickupItem.GetItemId();
        if (Subnautica.Server.Core.Server.Instance.Logices.Storage.TryPickupItem(component.WorldPickupItem, profile.InventoryItems))
        {
            foreach (KeyValuePair<string, ConstructionItem> baseWaterPark1 in Subnautica.Server.Core.Server.Instance.Logices.BaseWaterPark.GetBaseWaterParks())
            {
                Subnautica.Network.Models.Metadata.BaseWaterPark baseWaterPark2 = baseWaterPark1.Value.EnsureComponent<Subnautica.Network.Models.Metadata.BaseWaterPark>();
                if (baseWaterPark2.IsExistsCreatureEgg(itemId1))
                    return baseWaterPark2.RemoveCreatureEgg(itemId1);
                if (baseWaterPark2.GetCreature(itemId1) != null)
                    return baseWaterPark2.RemoveCreature(itemId1);
            }
        }
        return false;
    }

    public bool OnItemDrop(
      AuthorizationProfile profile,
      Subnautica.Network.Models.Metadata.BaseWaterPark waterPark,
      Subnautica.Network.Models.Metadata.BaseWaterPark packet,
      string waterParkId,
      out WorldDynamicEntity entity)
    {
        entity = (WorldDynamicEntity)null;
        if (packet.WorldPickupItem.Item.TechType.IsCreature())
        {
            string itemId = packet.WorldPickupItem.GetItemId();
            if (itemId.IsMultiplayerCreature() || !profile.InventoryItems.IsItemExists(itemId))
                return false;
            entity = Subnautica.Server.Core.Server.Instance.Logices.World.CreateDynamicEntity(packet.WorldPickupItem.GetItemId(), packet.WorldPickupItem.Item.TechType.ToCreatureEgg(), packet.Entity.Position, packet.Entity.Rotation, addInWorld: false);
            if (entity == null)
                return false;
            entity.SetComponent((NetworkDynamicEntityComponent)new Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.WaterParkCreature(packet.WaterParkCreature.AddedTime, waterParkId));
            if (!waterPark.AddCreature(entity))
                return false;
            this.RegisterCreature(waterParkId, entity);
            this.SendPackets();
            this.TriggerCreatureSpawn();
            return true;
        }
        if (waterPark.IsExistsCreatureEgg(packet.WorldPickupItem.GetItemId()))
            return false;
        waterPark.AddCreatureEgg(packet.WorldPickupItem.GetItemId());
        if (!Subnautica.Server.Core.Server.Instance.Logices.Storage.TryPickupToWorld(packet.WorldPickupItem, profile.InventoryItems, out entity))
            return false;
        entity.SetPositionAndRotation(packet.Entity.Position, packet.Entity.Rotation);
        entity.SetOwnership(profile.UniqueId);
        entity.SetDeployed(true);
        entity.SetComponent((NetworkDynamicEntityComponent)new Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.WaterParkCreature(Subnautica.Server.Core.Server.Instance.Logices.World.GetServerTimeAsDouble(), waterParkId));
        return true;
    }

    public void OnPlayerFullConnected(PlayerFullConnectedEventArgs ev)
    {
        this.Requests[ev.Player.UniqueId] = new HashSet<ushort>();
        foreach (KeyValuePair<ushort, BaseWaterParkCreatureItem> creatureWaterPark in this.CreatureWaterParks)
            this.AddPacketToQueue(ev.Player.UniqueId, creatureWaterPark.Key);
        this.SendPackets();
    }

    public void OnDeconstructionBegin(DeconstructionBeginArgs packet)
    {
        this.DeconstructionQueue.Enqueue(packet);
    }

    private bool IsPointInside(WaterPark waterPark, Vector3 position)
    {
        return waterPark is LargeRoomWaterPark largeRoomWaterPark ? largeRoomWaterPark.IsPointInside(position) : waterPark.IsPointInside(position);
    }

    private bool ConsumeDeconstruction(DeconstructionBeginArgs packet)
    {
        ConstructionItem construction1 = Subnautica.Server.Core.Server.Instance.Storages.Construction.GetConstruction(packet.UniqueId);
        if (construction1 == null || construction1.TechType != TechType.BaseWaterPark)
            return false;
        BaseDeconstructable componentByGameObject = Subnautica.API.Features.Network.Identifier.GetComponentByGameObject<BaseDeconstructable>(packet.UniqueId);
        WaterPark baseWaterPark1 = componentByGameObject != null ? componentByGameObject.GetBaseWaterPark() : (WaterPark)null;
        if (baseWaterPark1 == null)
            return false;
        ConstructionItem construction2 = this.GetWaterParkBelow(construction1.PlacePosition, construction1.LocalPosition) ?? this.GetWaterParkAbove(construction1.PlacePosition, construction1.LocalPosition);
        this.InitializeWaterParkChangePacket();
        foreach (KeyValuePair<string, ConstructionItem> baseWaterPark2 in this.GetBaseWaterParks())
        {
            Subnautica.Network.Models.Metadata.BaseWaterPark baseWaterPark3 = baseWaterPark2.Value.EnsureComponent<Subnautica.Network.Models.Metadata.BaseWaterPark>();
            if (baseWaterPark3 != null)
            {
                foreach (string egg in baseWaterPark3.GetEggs())
                {
                    WorldDynamicEntity dynamicEntity = Subnautica.Server.Core.Server.Instance.Storages.World.GetDynamicEntity(egg);
                    if (dynamicEntity != null)
                    {
                        Vector3 vector3 = dynamicEntity.Position.ToVector3();
                        bool flag1 = baseWaterPark2.Value.UniqueId == construction1.UniqueId;
                        bool flag2 = this.IsPointInside(baseWaterPark1, vector3);
                        if (flag2 | flag1)
                            baseWaterPark3.RemoveCreatureEgg(dynamicEntity.UniqueId);
                        if (flag2)
                            this.ChangeCreatureEggWaterPark(dynamicEntity, construction2);
                        else if (flag1)
                        {
                            ConstructionItem creaturePosition = this.FindWaterParkByCreaturePosition(vector3, construction1.UniqueId);
                            if (creaturePosition != null)
                                this.ChangeCreatureEggWaterPark(dynamicEntity, creaturePosition);
                            else
                                this.ChangeCreatureEggWaterPark(dynamicEntity, construction2);
                        }
                    }
                }
                foreach (WorldDynamicEntity creature1 in baseWaterPark3.GetCreatures())
                {
                    WorldDynamicEntity entity = creature1;
                    KeyValuePair<ushort, BaseWaterParkCreatureItem> keyValuePair = this.CreatureWaterParks.FirstOrDefault<KeyValuePair<ushort, BaseWaterParkCreatureItem>>((Func<KeyValuePair<ushort, BaseWaterParkCreatureItem>, bool>)(q => q.Value.EntityId == entity.UniqueId));
                    MultiplayerCreatureItem creature2;
                    if (keyValuePair.Value != null && Subnautica.Server.Core.Server.Instance.Logices.CreatureWatcher.TryGetCreature(keyValuePair.Key, out creature2))
                    {
                        Vector3 vector3 = creature2.Position.ToVector3();
                        bool flag3 = baseWaterPark2.Value.UniqueId == construction1.UniqueId;
                        bool flag4 = this.IsPointInside(baseWaterPark1, vector3);
                        if (flag4 | flag3)
                            baseWaterPark3.RemoveCreature(entity.UniqueId);
                        if (flag4)
                            this.ChangeCreatureWaterPark(creature2.Id, entity, construction2);
                        else if (flag3)
                        {
                            ConstructionItem creaturePosition = this.FindWaterParkByCreaturePosition(vector3, construction1.UniqueId);
                            if (creaturePosition != null)
                                this.ChangeCreatureWaterPark(creature2.Id, entity, creaturePosition);
                            else
                                this.ChangeCreatureWaterPark(creature2.Id, entity, construction2);
                        }
                    }
                }
            }
        }
        if (this.WaterParkChangePacket.Creatures.Count > 0)
            Subnautica.Server.Core.Server.SendPacketToAllClient((NetworkPacket)new MetadataComponentArgs()
            {
                TechType = TechType.BaseWaterPark,
                UniqueId = construction1.UniqueId,
                Component = (MetadataComponent)this.WaterParkChangePacket
            });
        if (Subnautica.Server.Core.Server.Instance.Storages.Construction.UpdateConstructionAmount(packet.UniqueId, 0.98f))
            Subnautica.Server.Core.Server.SendPacketToAllClient((NetworkPacket)packet);
        return true;
    }

    private ConstructionItem GetWaterParkAbove(
      ZeroVector3 centerPosition,
      ZeroVector3 centerLocalPosition)
    {
        return this.FindWaterPark(centerPosition, centerLocalPosition, true);
    }

    private ConstructionItem GetWaterParkBelow(
      ZeroVector3 centerPosition,
      ZeroVector3 centerLocalPosition)
    {
        return this.FindWaterPark(centerPosition, centerLocalPosition, false);
    }

    private ConstructionItem FindWaterPark(
      ZeroVector3 centerPosition,
      ZeroVector3 centerLocalPosition,
      bool isAbove)
    {
        float num = isAbove ? centerPosition.Y + global::Base.cellSize.y : centerPosition.Y - global::Base.cellSize.y;
        foreach (KeyValuePair<string, ConstructionItem> baseWaterPark in this.GetBaseWaterParks())
        {
            if ((double)baseWaterPark.Value.ConstructedAmount == 1.0 && (double)baseWaterPark.Value.PlacePosition.X == (double)centerPosition.X && (double)baseWaterPark.Value.PlacePosition.Y == (double)num && (double)baseWaterPark.Value.PlacePosition.Z == (double)centerPosition.Z && (double)Mathf.Abs(baseWaterPark.Value.LocalPosition.X - centerLocalPosition.X) <= (double)global::Base.cellSize.x && (double)Mathf.Abs(baseWaterPark.Value.LocalPosition.Z - centerLocalPosition.Z) <= (double)global::Base.cellSize.z)
                return baseWaterPark.Value;
        }
        return (ConstructionItem)null;
    }

    private ConstructionItem FindWaterParkByCreaturePosition(
      Vector3 creaturePosition,
      string ignoreUniqueId)
    {
        foreach (KeyValuePair<string, ConstructionItem> baseWaterPark1 in this.GetBaseWaterParks())
        {
            if (!(baseWaterPark1.Value.UniqueId == ignoreUniqueId) && (double)baseWaterPark1.Value.ConstructedAmount == 1.0)
            {
                BaseDeconstructable componentByGameObject = Subnautica.API.Features.Network.Identifier.GetComponentByGameObject<BaseDeconstructable>(baseWaterPark1.Value.UniqueId);
                WaterPark baseWaterPark2 = componentByGameObject != null ? componentByGameObject.GetBaseWaterPark() : (WaterPark)null;
                if (baseWaterPark2 == null && baseWaterPark2.IsPointInside(creaturePosition))
                    return baseWaterPark1.Value;
            }
        }
        return (ConstructionItem)null;
    }

    private void InitializeWaterParkChangePacket()
    {
        this.WaterParkChangePacket = new Subnautica.Network.Models.Metadata.BaseWaterPark()
        {
            ProcessType = BaseWaterParkProcessType.WaterParkChange,
            Creatures = new List<WorldDynamicEntity>()
        };
    }

    private bool ChangeCreatureEggWaterPark(
      WorldDynamicEntity entity,
      ConstructionItem construction)
    {
        if (!this.WaterParkChangePacket.Creatures.Any<WorldDynamicEntity>((Func<WorldDynamicEntity, bool>)(q => q.UniqueId == entity.UniqueId)))
            this.WaterParkChangePacket.Creatures.Add(new WorldDynamicEntity()
            {
                UniqueId = entity.UniqueId,
                ParentId = construction?.UniqueId
            });
        if (construction != null)
        {
            Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.WaterParkCreature component = entity.Component.GetComponent<Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.WaterParkCreature>();
            if (component != null)
                component.WaterParkId = construction.UniqueId;
            construction.EnsureComponent<Subnautica.Network.Models.Metadata.BaseWaterPark>().AddCreatureEgg(entity.UniqueId, true);
        }
        else
            Subnautica.Server.Core.Server.Instance.Storages.World.RemoveDynamicEntity(entity.UniqueId);
        return true;
    }

    public bool ChangeCreatureWaterPark(
      ushort creatureId,
      WorldDynamicEntity entity,
      ConstructionItem construction)
    {
        if (!this.WaterParkChangePacket.Creatures.Any<WorldDynamicEntity>((Func<WorldDynamicEntity, bool>)(q => (int)q.Id == (int)creatureId)))
            this.WaterParkChangePacket.Creatures.Add(new WorldDynamicEntity()
            {
                Id = creatureId,
                ParentId = construction?.UniqueId
            });
        if (construction != null)
        {
            construction.EnsureComponent<Subnautica.Network.Models.Metadata.BaseWaterPark>().AddCreature(entity);
            this.CreatureWaterParks[creatureId].WaterParkId = construction.UniqueId;
        }
        else
        {
            this.CreatureWaterParks.Remove(creatureId);
            Subnautica.Server.Core.Server.Instance.Logices.CreatureWatcher.UnRegisterCreature(creatureId);
        }
        return true;
    }

    private Subnautica.Network.Models.Metadata.BaseWaterPark GetWaterPark(string uniqueId)
    {
        return Subnautica.Server.Core.Server.Instance.Storages.Construction.GetConstruction(uniqueId)?.EnsureComponent<Subnautica.Network.Models.Metadata.BaseWaterPark>();
    }

    private void TriggerCreatureSpawn()
    {
        Subnautica.Server.Core.Server.Instance.Logices.CreatureWatcher.ImmediatelyTrigger();
    }

    private void RegisterCreature(string waterParkId, WorldDynamicEntity entity, bool destroyEgg = false)
    {
        ushort creatureId = Subnautica.Server.Core.Server.Instance.Logices.CreatureWatcher.RegisterCreature(entity.TechType, entity.Position, entity.Rotation);
        if (creatureId <= (ushort)0)
            return;
        this.SpawnCreatureInWaterPark(waterParkId, creatureId, entity.UniqueId, destroyEgg);
    }

    private void AddPacketToQueue(string playerId, ushort creatureId)
    {
        HashSet<ushort> ushortSet;
        if (!this.Requests.TryGetValue(playerId, out ushortSet))
            return;
        ushortSet.Add(creatureId);
    }

    private void SpawnCreatureInWaterPark(
      string waterParkId,
      ushort creatureId,
      string entityId,
      bool destroyEgg = false)
    {
        this.CreatureWaterParks[creatureId] = new BaseWaterParkCreatureItem(waterParkId, entityId, destroyEgg);
        foreach (AuthorizationProfile player in Subnautica.Server.Core.Server.Instance.GetPlayers())
            this.AddPacketToQueue(player.UniqueId, creatureId);
    }

    private void SendPackets()
    {
        foreach (KeyValuePair<string, HashSet<ushort>> request in this.Requests)
        {
            AuthorizationProfile player = Subnautica.Server.Core.Server.Instance.GetPlayer(request.Key);
            if (player != null && request.Value.Any<ushort>())
            {
                Dictionary<string, Subnautica.Network.Models.Metadata.BaseWaterPark> dictionary = new Dictionary<string, Subnautica.Network.Models.Metadata.BaseWaterPark>();
                foreach (ushort key in request.Value)
                {
                    BaseWaterParkCreatureItem parkCreatureItem;
                    if (this.CreatureWaterParks.TryGetValue(key, out parkCreatureItem))
                    {
                        Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.WaterParkCreature entity = parkCreatureItem.GetEntity();
                        if (entity != null)
                        {
                            if (!dictionary.TryGetValue(parkCreatureItem.WaterParkId, out Subnautica.Network.Models.Metadata.BaseWaterPark _))
                                dictionary[parkCreatureItem.WaterParkId] = new Subnautica.Network.Models.Metadata.BaseWaterPark()
                                {
                                    ProcessType = BaseWaterParkProcessType.Spawn,
                                    Creatures = new List<WorldDynamicEntity>()
                                };
                            dictionary[parkCreatureItem.WaterParkId].Creatures.Add(new WorldDynamicEntity()
                            {
                                Id = key,
                                AddedTime = (float)entity.AddedTime,
                                UniqueId = parkCreatureItem.IsDestroyEgg ? parkCreatureItem.EntityId : (string)null
                            });
                        }
                    }
                }
                foreach (KeyValuePair<string, Subnautica.Network.Models.Metadata.BaseWaterPark> keyValuePair in dictionary)
                {
                    MetadataComponentArgs packet = new MetadataComponentArgs()
                    {
                        TechType = TechType.BaseWaterPark,
                        UniqueId = keyValuePair.Key,
                        Component = (MetadataComponent)keyValuePair.Value
                    };
                    player.SendPacket((NetworkPacket)packet);
                }
                request.Value.Clear();
                dictionary.Clear();
            }
        }
    }

    public List<KeyValuePair<string, ConstructionItem>> GetBaseWaterParks()
    {
        return Subnautica.Server.Core.Server.Instance.Storages.Construction.Storage.Constructions.Where<KeyValuePair<string, ConstructionItem>>((Func<KeyValuePair<string, ConstructionItem>, bool>)(q => (double)q.Value.ConstructedAmount == 1.0 && q.Value.TechType == TechType.BaseWaterPark)).ToList<KeyValuePair<string, ConstructionItem>>();
    }
}
