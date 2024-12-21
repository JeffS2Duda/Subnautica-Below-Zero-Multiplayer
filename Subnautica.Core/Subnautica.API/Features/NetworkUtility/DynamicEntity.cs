namespace Subnautica.API.Features.NetworkUtility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features.Helper;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;

    using UnityEngine;

    using WorldEntityModel = Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;

    public class DynamicEntity
    {
        private Dictionary<string, WorldDynamicEntity> Entities { get; set; } = new Dictionary<string, WorldDynamicEntity>();

        public HashSet<ushort> ActivatedEntities { get; set; } = new HashSet<ushort>();

        public float VisibilityDistance { get; set; } = 5000f;

        public float PhysicsDistance { get; set; } = 2500f;

        public void Spawn(WorldDynamicEntity entity, Action<ItemQueueProcess, Pickupable, GameObject> onEntitySpawned, object customProperty = null, object customProperty2 = null, bool ignoreDynamicCheck = false)
        {
            var action = new ItemQueueAction();
            action.RegisterProperty("OnEntitySpawned"   , onEntitySpawned);
            action.RegisterProperty("CustomProperty"    , customProperty);
            action.RegisterProperty("CustomProperty2"   , customProperty2);
            action.RegisterProperty("IgnoreDynamicCheck", ignoreDynamicCheck);
            action.RegisterProperty("Entity", entity);
            action.OnEntitySpawned = this.OnEntitySpawned;

            if (entity.Item != null)
            {
                Entity.SpawnToQueue(entity.Item, entity.UniqueId, new ZeroTransform(entity.Position, entity.Rotation), action);
            }
            else
            {
                Entity.SpawnToQueue(entity.TechType, entity.UniqueId, new ZeroTransform(entity.Position, entity.Rotation), action);
            }
        }

        public void Remove(string itemId)
        {
            var action = new ItemQueueAction();
            action.OnEntityRemoved = this.OnEntityRemoved;
            action.RegisterProperty("ItemId", itemId);

            Entity.RemoveToQueue(itemId, action);
        }

        public void OnEntityRemoved(ItemQueueProcess item)
        {
            Network.DynamicEntity.RemoveEntity(item.Action.GetProperty<string>("ItemId"));
        }

        public void OnEntitySpawned(ItemQueueProcess item, Pickupable pickupable, GameObject gameObject)
        {
            var entity = item.Action.GetProperty<WorldDynamicEntity>("Entity");
            var ignore = item.Action.GetProperty<bool>("IgnoreDynamicCheck");
            if (ignore == false)
            {
                this.SetEntity(entity);
            }

            var action = item.Action.GetProperty<Action<ItemQueueProcess, Pickupable, GameObject>>("OnEntitySpawned");
            if (action != null)
            {
                action.Invoke(item, pickupable, gameObject);
            }
        }

        public void AddEntity(WorldDynamicEntity entity)
        {
            if (!this.Entities.ContainsKey(entity.UniqueId))
            {
                this.Entities.Add(entity.UniqueId, entity);
            }
        }

        public void SetEntity(WorldDynamicEntity entity)
        {
            this.Entities[entity.UniqueId] = entity;
        } 

        public void SetEntityUsingByPlayer(ushort entityId, bool status)
        {
            if (entityId > 0)
            {
                var entity = this.GetEntity(entityId);
                if (entity != null)
                {
                    entity.IsUsingByPlayer = status;
                }
            }
        } 

        public void RemoveEntity(string uniqueId)
        {
            if (this.Entities.TryGetValue(uniqueId, out var entity))
            {
                this.ActivatedEntities.Remove(entity.Id);
                this.Entities.Remove(uniqueId);
            }
        }

        public Dictionary<string, WorldDynamicEntity> GetEntities()
        {
            return this.Entities;
        }

        public HashSet<ushort> GetActivatedEntityIds()
        {
            return this.ActivatedEntities;
        }

        public bool IsEntityActivated(ushort entityId)
        {
            return this.ActivatedEntities.Any(q => q == entityId);
        }

        public void ActivateEntity(ushort entityId)
        {
            this.ActivatedEntities.Add(entityId);
        }

        public void DisableEntity(ushort entityId)
        {
            this.ActivatedEntities.Remove(entityId);
        }

        public bool IsMine(string uniqueId)
        {
            var entity = this.GetEntity(uniqueId);
            if (entity == null)
            {
                return false;
            }

            return entity.IsMine(ZeroPlayer.CurrentPlayer.UniqueId);
        }

        public TechType GetTechType(string uniqueId)
        {
            var entity = this.GetEntity(uniqueId);
            if (entity == null)
            {
                return TechType.None;
            }

            return entity.TechType;
        }

        public WorldDynamicEntity GetEntity(ushort id)
        {
            return this.Entities.FirstOrDefault(q => q.Value.Id == id).Value;
        }

        public WorldDynamicEntity GetEntity(string uniqueId)
        {
            if (uniqueId.IsNull())
            {
                return null;
            }

            this.Entities.TryGetValue(uniqueId, out var entity);
            return entity;
        }

        public bool HasEntity(string uniqueId)
        {
            return this.Entities.ContainsKey(uniqueId);
        }

        public void ChangePosition(ushort id, ZeroVector3 position, ZeroQuaternion rotation)
        {
            var entity = this.Entities.FirstOrDefault(q => q.Value.Id == id);
            if (entity.Value != null)
            {
                entity.Value.Position = position;
                entity.Value.Rotation = rotation;
            }
        }

        public void ChangeOwnership(ushort id, string playerId)
        {
            var entity = this.Entities.FirstOrDefault(q => q.Value.Id == id);
            if (entity.Value != null)
            {
                entity.Value.SetOwnership(playerId);
            }
        }

        public bool ToggleKinematic(WorldDynamicEntity entity, ZeroKinematicState kinematicState)
        {
            if (kinematicState != ZeroKinematicState.Ignore)
            {
                entity.UpdateGameObject();

                if (entity.Rigidbody)
                {
                    if (kinematicState == ZeroKinematicState.Kinematic)
                    {
                        entity.Rigidbody.SetKinematic();
                    }
                    else
                    {
                        entity.Rigidbody.SetNonKinematic();
                    }

                    entity.CacheKinematicStatus(); 
                    return true;
                }
            }

            return false;
        }

        public ZeroKinematicState CalculateKinematic(WorldDynamicEntity entity, ZeroVector3 playerPosition, string playerUniqueId)
        {
            if (entity.IsUsingByPlayer)
            {
                if (entity.IsMine(playerUniqueId))
                {
                    if (entity.TechType == TechType.SpyPenguin)
                    {
                        return ZeroKinematicState.Kinematic;
                    }

                    return ZeroKinematicState.NonKinematic;
                }

                return ZeroKinematicState.Kinematic;
            }

            if (entity.TechType == TechType.SpyPenguin)
            {
                return ZeroKinematicState.Kinematic;
            }

            if (entity.TechType == TechType.PipeSurfaceFloater)
            {
                return ZeroKinematicState.Ignore;
            }
            
            if (entity.TechType == TechType.Beacon)
            {
                var beacon = entity.Component.GetComponent<WorldEntityModel.Beacon>();
                if (beacon == null || beacon.IsDeployedOnLand)
                {
                    return ZeroKinematicState.Kinematic;
                }
            }

            if (entity.IsDeployed)
            {
                switch (entity.TechType)
                {
                    case TechType.LEDLight:
                    case TechType.Thumper:
                        return ZeroKinematicState.Kinematic;
                }
            }

            if (!entity.IsMine(playerUniqueId) || !entity.IsPhysicSimulateable(playerPosition))
            {
                return ZeroKinematicState.Kinematic;
            }

            if (!Network.CellManager.IsLoaded(entity.Position))
            {
                return ZeroKinematicState.Kinematic;
            }

            if (Network.Session.IsInSeaTruck && entity.TechType.IsSeaTruckModule(true))
            {
                return ZeroKinematicState.Kinematic;
            }

            if (entity.TechType.IsVehicle())
            {
                if (entity.GameObject.TryGetComponent<VFXConstructing>(out var vfx))
                {
                    if (!vfx.IsConstructed())
                    {
                        return ZeroKinematicState.Kinematic;
                    }
                }
            }

            return ZeroKinematicState.NonKinematic;
        }

        public void Dispose()
        {
            this.Entities.Clear();
            this.ActivatedEntities.Clear();
        }
    }
}
