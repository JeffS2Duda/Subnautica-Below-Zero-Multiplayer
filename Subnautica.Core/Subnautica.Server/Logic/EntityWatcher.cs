namespace Subnautica.Server.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    using Core;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Server.Abstracts;

    using ServerModel = Subnautica.Network.Models.Server;

    public class EntityWatcher : BaseLogic
    {
        public StopwatchItem Timing { get; set; } = new StopwatchItem(1000f);

        public Dictionary<string, List<ushort>> ChangedEntities { get; set; } = new Dictionary<string, List<ushort>>();

        public override void OnStart()
        {
            foreach (var entity in Server.Instance.Storages.World.Storage.DynamicEntities)
            {
                entity.IsUsingByPlayer = false;
                entity.SetOwnership(null);
            }
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
            if (this.Timing.IsFinished() && API.Features.World.IsLoaded)
            {
                this.Timing.Restart();

                foreach (var entity in Server.Instance.Storages.World.Storage.DynamicEntities)
                {
                    if (entity.IsParentExist())
                    {
                        this.UpdateEntityPosition(entity);
                        continue;
                    }

                    if (entity.IsUsingByPlayer)
                    {
                        continue;
                    }

                    if (entity.OwnershipId.IsNotNull())
                    {
                        var owner = Server.Instance.GetPlayer(entity.OwnershipId);
                        if (owner != null && entity.IsVisible(owner.Position))
                        {
                            continue;
                        }
                    }

                    entity.OwnershipId = this.FindEntityOwnership(entity);

                    if (!string.IsNullOrEmpty(entity.OwnershipId))
                    {
                        this.AddChangedEntity(entity.OwnershipId, entity.Id);
                    }
                }

                this.SendPacketToAllClient();
            }
        }

        public void SendPacketToAllClient()
        {
            if (this.ChangedEntities.Count > 0)
            {
                ServerModel.WorldDynamicEntityOwnershipChangedArgs request = new ServerModel.WorldDynamicEntityOwnershipChangedArgs()
                {
                    Entities  = this.ChangedEntities,
                };

                Core.Server.SendPacketToAllClient(request);

                this.ChangedEntities.Clear();
            }
        }

        public string FindEntityOwnership(WorldDynamicEntity entity)
        {
            var ownershipId  = string.Empty;
            var lastDistance = 99999f;
            var serverRange  = Network.DynamicEntity.VisibilityDistance * 0.75f;

            foreach (var player in Server.Instance.GetPlayers())
            {
                if (player.IsFullConnected)
                {
                    if (entity.IsVisible(player.Position))
                    {
                        var distance = player.Position.Distance(entity.Position);

                        if (player.IsHost && distance < serverRange)
                        {
                            return player.UniqueId;
                        }
                        else if (distance < lastDistance)
                        {
                            ownershipId  = player.UniqueId;
                            lastDistance = distance;
                        }
                    }
                }
            }

            return ownershipId;
        }

        private void UpdateEntityPosition(WorldDynamicEntity entity)
        {
            var gameObject = Network.Identifier.GetGameObject(entity.UniqueId);
            if (gameObject)
            {
                entity.Position = gameObject.transform.position.ToZeroVector3();
                entity.Rotation = gameObject.transform.rotation.ToZeroQuaternion();
            }
        }

        public bool AddChangedEntity(string ownershipId, ushort entityId, bool isIgnoreEmpty = false)
        {
            if (string.IsNullOrEmpty(ownershipId) && !isIgnoreEmpty)
            {
                return false;
            }

            if (!this.ChangedEntities.ContainsKey(ownershipId))
            {
                this.ChangedEntities[ownershipId] = new List<ushort>();
            }

            this.ChangedEntities[ownershipId].Add(entityId);
            return true;
        }

        public void ChangeEntityOwnership(WorldDynamicEntity entity, string newOwnershipId, bool autoSend = true)
        {
            entity.OwnershipId = newOwnershipId;

            this.AddChangedEntity(entity.OwnershipId, entity.Id);

            if (autoSend)
            {
                this.SendPacketToAllClient();
            }
        }

        public void RemoveOwnershipByPlayer(string playerId)
        {
            foreach (var entity in Server.Instance.Storages.World.Storage.DynamicEntities.Where(q => q.OwnershipId == playerId))
            {
                entity.IsUsingByPlayer = false;
                entity.OwnershipId     = this.FindEntityOwnership(entity);
                
                if (string.IsNullOrEmpty(entity.OwnershipId))
                {
                    this.RemoveWatcherByEntity(entity);
                }
                else
                {
                    this.AddChangedEntity(entity.OwnershipId, entity.Id);
                }
            }

            this.SendPacketToAllClient();
        }

        public void RemoveWatcherByEntity(WorldDynamicEntity entity)
        {
            entity.OwnershipId = null;

            this.AddChangedEntity("", entity.Id);
        }
    }
}
