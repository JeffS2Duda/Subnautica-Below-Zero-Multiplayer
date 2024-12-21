namespace Subnautica.Server.Storage
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Core;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Metadata;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity;
    using Subnautica.Network.Structures;
    using Subnautica.Server.Abstracts;
    using Subnautica.Server.Extensions;
    using System;
    using System.IO;
    using System.Linq;
    using Metadata = Subnautica.Network.Models.Metadata;
    using WorldChildrens = Subnautica.Network.Models.Storage.World.Childrens;
    using WorldEntityModel = Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;
    using WorldStorage = Subnautica.Network.Models.Storage.World;

    public class World : BaseStorage
    {
        public WorldStorage.World Storage { get; set; }

        public override void Start(string serverId)
        {
            this.ServerId = serverId;
            this.FilePath = Paths.GetMultiplayerServerSavePath(this.ServerId, "World.bin");
            this.InitializePath();
            this.Load();
        }

        public override void Load()
        {
            if (File.Exists(this.FilePath))
            {
                lock (this.ProcessLock)
                {
                    try
                    {
                        this.Storage = NetworkTools.Deserialize<WorldStorage.World>(File.ReadAllBytes(this.FilePath));
                    }
                    catch (Exception e)
                    {
                        Log.Error($"World.Load: {e}");
                    }
                }
            }
            else
            {
                this.Storage = new WorldStorage.World();
                this.SaveToDisk();
            }

            if (Core.Server.DEBUG)
            {
                Log.Info("World Detail: ");
                Log.Info("---------------------------------------------------------------");
                Log.Info(String.Format("ServerTime         : {0}", this.Storage.ServerTime));
                Log.Info("---------------------------------------------------------------");
            }
        }

        public override void SaveToDisk()
        {
            lock (this.ProcessLock)
            {
                this.WriteToDisk(this.Storage);
            }
        }

        public bool TryGetSupplyDrop(out WorldChildrens.SupplyDrop supplyDrop)
        {
            supplyDrop = this.Storage.SupplyDrops.Where(q => q.Key == API.Constants.SupplyDrop.Lifepod).FirstOrDefault();
            return supplyDrop != null;
        }

        public bool AddSeaTruckConnection(string frontModuleId, string backModuleId, bool checkBackModule = true)
        {
            var frontModule = Core.Server.Instance.Storages.World.GetDynamicEntity(frontModuleId);
            if (frontModule == null || !frontModule.TechType.IsSeaTruckModule(true))
            {
                return false;
            }

            if (checkBackModule)
            {
                var backModule = Core.Server.Instance.Storages.World.GetDynamicEntity(backModuleId);
                if (backModule == null || !backModule.TechType.IsSeaTruckModule(true))
                {
                    return false;
                }
            }

            lock (this.ProcessLock)
            {
                if (this.Storage.SeaTruckConnections.ContainsKey(frontModuleId))
                {
                    return false;
                }

                if (this.Storage.SeaTruckConnections.Any(q => q.Value == backModuleId))
                {
                    return false;
                }


                frontModule.SetParent(backModuleId);

                Core.Server.Instance.Logices.EntityWatcher.RemoveWatcherByEntity(frontModule);

                this.Storage.SeaTruckConnections[frontModuleId] = backModuleId;
                return true;
            }
        }

        public string RemoveSeaTruckConnection(string frontModuleId, bool checkModule = true)
        {
            if (checkModule)
            {
                var frontModule = Core.Server.Instance.Storages.World.GetDynamicEntity(frontModuleId);
                if (frontModule == null)
                {
                    return null;
                }
            }

            var connection = this.Storage.SeaTruckConnections.FirstOrDefault(q => q.Value == frontModuleId);
            if (connection.Value == null)
            {
                return null;
            }

            this.Storage.SeaTruckConnections.Remove(connection.Key);

            var backModule = Core.Server.Instance.Storages.World.GetDynamicEntity(connection.Key);
            if (backModule == null)
            {
                return null;
            }

            backModule.SetParent(null);
            return connection.Key;
        }

        public bool TryGetBase(string baseId, out Base baseComponent)
        {
            lock (this.ProcessLock)
            {
                var baseData = this.Storage.Bases.Where(q => q.BaseId == baseId).FirstOrDefault();
                if (baseData != null)
                {
                    baseComponent = baseData;
                }
                else
                {
                    baseComponent = new Base()
                    {
                        BaseId = baseId
                    };

                    this.Storage.Bases.Add(baseComponent);
                }

                return true;
            }
        }

        public void RemoveBase(string baseId)
        {
            lock (this.ProcessLock)
            {
                this.Storage.Bases.RemoveAll(q => q.BaseId == baseId);
            }
        }

        public void RemoveBase(Base baseComp)
        {
            lock (this.ProcessLock)
            {
                this.Storage.Bases.Remove(baseComp);
            }
        }

        public bool ActivateTeleportPortal(string uniqueId)
        {
            lock (this.ProcessLock)
            {
                if (this.Storage.ActivatedPrecursorTeleporters.Contains(uniqueId))
                {
                    return false;
                }

                this.Storage.ActivatedPrecursorTeleporters.Add(uniqueId);
                return true;
            }
        }

        public bool UpdateConstructions(byte[] constructions)
        {
            lock (this.ProcessLock)
            {
                this.Storage.Constructions = constructions;
                return true;
            }
        }

        public bool AddWorldDynamicEntity(WorldDynamicEntity entity)
        {
            lock (this.ProcessLock)
            {
                if (this.Storage.DynamicEntities.Any(q => q.UniqueId == entity.UniqueId))
                {
                    return false;
                }

                this.Storage.DynamicEntities.Add(entity);
                return true;
            }
        }

        public WorldDynamicEntity GetDynamicEntity(string uniqueId)
        {
            if (uniqueId.IsNull())
            {
                return null;
            }

            lock (this.ProcessLock)
            {
                return this.Storage.DynamicEntities.FirstOrDefault(q => q.UniqueId == uniqueId);
            }
        }

        public WorldDynamicEntity GetVehicle(string uniqueId, bool ignoreMoonpool = false)
        {
            lock (this.ProcessLock)
            {
                var vehicle = this.Storage.DynamicEntities.FirstOrDefault(q => q.UniqueId == uniqueId);

                foreach (var item in Server.Core.Server.Instance.Storages.Construction.Storage.Constructions.Where(q => q.Value.TechType == TechType.BaseMoonpool || q.Value.TechType == TechType.BaseMoonpoolExpansion))
                {
                    var moonpool = item.Value.EnsureComponent<Metadata.BaseMoonpool>();
                    if (moonpool.IsDocked && moonpool.Vehicle.UniqueId == uniqueId)
                    {
                        if (ignoreMoonpool)
                        {
                            return null;
                        }

                        return moonpool.Vehicle;
                    }
                    else if (moonpool.ExpansionManager.IsTailDocked() && ignoreMoonpool && vehicle != null)
                    {
                        if (vehicle.UniqueId == moonpool.ExpansionManager.TailId)
                        {
                            return null;
                        }

                        foreach (var fontModule in vehicle.GetSeaTruckFrontModule())
                        {
                            if (fontModule != null && fontModule.UniqueId == moonpool.ExpansionManager.TailId)
                            {
                                return null;
                            }
                        }
                    }
                }

                if (vehicle != null)
                {
                    return vehicle;
                }

                foreach (var item in this.Storage.DynamicEntities.Where(q => q.TechType == TechType.SeaTruckDockingModule))
                {
                    var component = item.Component.GetComponent<WorldEntityModel.SeaTruckDockingModule>();
                    if (component.IsDocked() && component.Vehicle.UniqueId == uniqueId)
                    {
                        return component.Vehicle;
                    }
                }

                return null;
            }
        }

        public bool RemoveCosmeticItem(string uniqueId)
        {
            lock (this.ProcessLock)
            {
                return this.Storage.CosmeticItems.RemoveWhere(q => q.StorageItem.ItemId == uniqueId) > 0;
            }
        }

        public CosmeticItem GetCosmeticItem(string uniqueId)
        {
            lock (this.ProcessLock)
            {
                return this.Storage.CosmeticItems.FirstOrDefault(q => q.StorageItem.ItemId == uniqueId);
            }
        }

        public bool AddCosmeticItem(string uniqueId, string baseId, TechType techType, ZeroVector3 position, ZeroQuaternion rotation)
        {
            lock (this.ProcessLock)
            {
                if (this.Storage.CosmeticItems.Any(q => q.StorageItem.ItemId == uniqueId))
                {
                    return false;
                }

                return this.Storage.CosmeticItems.Add(new CosmeticItem(StorageItem.Create(uniqueId, techType), baseId, position, rotation));
            }
        }

        public T GetDynamicEntityComponent<T>(string uniqueId)
        {
            var entity = this.GetDynamicEntity(uniqueId);
            if (entity == null || entity.Component == null)
            {
                return default;
            }

            return entity.Component.GetComponent<T>();
        }

        public WorldDynamicEntity GetDynamicEntity(ushort id)
        {
            lock (this.ProcessLock)
            {
                return this.Storage.DynamicEntities.FirstOrDefault(q => q.Id == id);
            }
        }

        public bool RemoveDynamicEntity(string uniqueId)
        {
            lock (this.ProcessLock)
            {
                return this.Storage.DynamicEntities.RemoveWhere(q => q.UniqueId == uniqueId) > 0;
            }
        }

        public bool AddDiscoveredResource(TechType techType)
        {
            lock (this.ProcessLock)
            {
                return this.Storage.DiscoveredTechTypes.Add(techType);
            }
        }

        public bool AddJukeboxDisk(string trackFile)
        {
            lock (this.ProcessLock)
            {
                if (this.Storage.JukeboxDisks.Contains(trackFile))
                {
                    return false;
                }

                this.Storage.JukeboxDisks.Add(trackFile);

                Core.Server.Instance.Logices.Jukebox.SortPlaylist();
                return true;
            }
        }

        public bool SetPersistentEntity(NetworkWorldEntityComponent entity)
        {
            lock (this.ProcessLock)
            {
                if (entity == null || entity.UniqueId.IsNull())
                {
                    return false;
                }

                this.Storage.PersistentEntities[entity.UniqueId] = entity;
                return true;
            }
        }

        public NetworkWorldEntityComponent GetPersistentEntity(string uniqueId)
        {
            lock (this.ProcessLock)
            {
                if (this.Storage.PersistentEntities.TryGetValue(uniqueId, out var entity))
                {
                    return entity;
                }

                return null;
            }
        }

        public T GetPersistentEntity<T>(string uniqueId)
        {
            var entity = this.GetPersistentEntity(uniqueId);
            if (entity == null)
            {
                return default;
            }

            return entity.GetComponent<T>();
        }

        public bool IsPersistentEntityExists(string uniqueId)
        {
            lock (this.ProcessLock)
            {
                return this.Storage.PersistentEntities.ContainsKey(uniqueId);
            }
        }

        public bool AddDisablePersistentEntity(string uniqueId)
        {
            lock (this.ProcessLock)
            {
                if (this.Storage.PersistentEntities.TryGetValue(uniqueId, out var component))
                {
                    if (component.IsSpawnable)
                    {
                        component.DisableSpawn();
                        return true;
                    }

                    return false;
                }

                var entity = new StaticEntity()
                {
                    UniqueId = uniqueId
                };

                entity.DisableSpawn();

                this.SetPersistentEntity(entity);
                return true;
            }
        }

        public bool DisableSlot(string uniqueId)
        {
            lock (this.ProcessLock)
            {
                var spawnPoint = this.Storage.SpawnPoints.FirstOrDefault(q => q.SlotId == uniqueId.WorldStreamerToSlotId());
                if (spawnPoint == null)
                {
                    return false;
                }

                var currentTime = Server.Core.Server.Instance.Logices.World.GetServerTime();
                if (!spawnPoint.IsRespawnable(currentTime))
                {
                    return false;
                }

                spawnPoint.NextRespawnTime = spawnPoint.GetNextRespawnTime(currentTime);
                return true;
            }
        }

        public float GetSlotNextRespawnTime(string uniqueId)
        {
            lock (this.ProcessLock)
            {
                var spawnPoint = this.Storage.SpawnPoints.FirstOrDefault(q => q.SlotId == uniqueId.WorldStreamerToSlotId());
                if (spawnPoint == null)
                {
                    return -1f;
                }

                return spawnPoint.NextRespawnTime;
            }
        }
    }
}
