namespace Subnautica.API.Features.NetworkUtility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features.Helper;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    using UnityEngine;

    using UWE;

    using Metadata = Subnautica.Network.Models.Metadata;

    public class Storage
    {
        private readonly List<StorageProcessItem> Queue = new List<StorageProcessItem>();
        
        private readonly WaitForSecondsRealtime WaitTime = new WaitForSecondsRealtime(0.25f);

        private bool IsRunning { get; set; } = false;

        public double MaxDelay = 2.5;

        public void InitializeStorage(string containerId, Metadata.StorageContainer storageContainer, Action<ItemQueueProcess, Pickupable, GameObject> onEntitySpawned = null, object customProperty = null)
        {
            if (storageContainer != null)
            {
                var container = this.GetItemContainer(containerId);
                if (container != null)
                {
                    var action = new ItemQueueAction();
                    action.OnEntitySpawned = onEntitySpawned;
                    action.RegisterProperty("CustomProperty", customProperty);

                    foreach (var item in storageContainer.Items)
                    {
                        Entity.SpawnToQueue(item.Item, item.ItemId, container, action);
                    }
                }
            }
        }

        public void AddItemToStorage(string containerId, byte playerId, WorldPickupItem worldPickupItem, Action<ItemQueueProcess, Pickupable, GameObject> onEntitySpawned = null, object customProperty = null)
        {
            this.Queue.Add(new StorageProcessItem()
            {
                OwnerId         = playerId,
                ContainerId     = containerId,
                WorldPickupItem = worldPickupItem,
                OnEntitySpawned = onEntitySpawned,
                CustomProperty  = customProperty
            });

            this.ConsumeQueue();
        }

        public void AddItemToInventory(byte playerId, WorldPickupItem pickupItem, Action<ItemQueueProcess, Pickupable, GameObject> onEntitySpawned = null, object customProperty = null, bool showNotify = false)
        {
            World.DestroyPickupItem(pickupItem);

            if (ZeroPlayer.IsPlayerMine(playerId))
            {
                if (showNotify)
                {
                    pickupItem.Item.TechType.ShowPickupNotify();
                }

                if (onEntitySpawned == null)
                {
                    World.SpawnPickupItemToInventory(pickupItem);
                }
                else
                {
                    var action = new ItemQueueAction();
                    action.OnEntitySpawned = onEntitySpawned;
                    action.RegisterProperty("CustomProperty", customProperty);

                    World.SpawnPickupItemToInventory(pickupItem, action);
                }
            }
        }

        private bool ConsumeQueue()
        {
            if (this.IsRunning)
            {
                return false;
            }

            CoroutineHost.StartCoroutine(this.ConsumeQueueAsync());
            return true;
        }

        private IEnumerator ConsumeQueueAsync()
        {
            this.IsRunning = true;

            while (this.Queue.Count > 0)
            {
                foreach (var item in this.Queue.ToList())
                {
                    if (item.IsFirstTime())
                    {
                        item.FirstCheckTime = Network.Session.GetWorldTime();

                        if (this.AddItemToStorage(item, true))
                        {
                            this.Queue.Remove(item);
                            continue;
                        }
                    }

                    if (item.IsDelayed())
                    {
                        Log.Error("ITEM VERY DELAYED. MAYBE DE-SYNC...");
                        this.Queue.Remove(item);
                    }
                    else 
                    {
                        if (this.AddItemToStorage(item, false))
                        {
                            this.Queue.Remove(item);
                        }
                    }
                }

                if (this.Queue.Count > 0)
                {
                    yield return this.WaitTime;
                }
            }

            this.IsRunning = false;
        }

        private bool AddItemToStorage(StorageProcessItem item, bool destroyItem)
        {
            if (destroyItem && ZeroPlayer.IsPlayerMine(item.OwnerId))
            {
                Entity.RemoveToQueue(item.WorldPickupItem.Item.ItemId);
            }

            var container = this.GetItemContainer(item.ContainerId);
            if (container == null)
            {
                return false;
            }

            var action = new ItemQueueAction();
            action.OnEntitySpawned = item.OnEntitySpawned;
            action.RegisterProperty("CustomProperty", item.CustomProperty);

            World.SpawnPickupItem(item.WorldPickupItem, container, action);
            return true;
        }

        private ItemsContainer GetItemContainer(string containerId)
        {
            var gameObject = Network.Identifier.GetGameObject(containerId, true);
            if (gameObject == null)
            {
                return null;
            }

            if (gameObject.TryGetComponent<global::BaseBioReactorGeometry>(out var bioReactor))
            {
                if (bioReactor.GetModule())
                {
                    return bioReactor.GetModule().container;
                }
            }
            else if (gameObject.TryGetComponent<global::StorageContainer>(out var storageContainer))
            {
                return storageContainer.container;
            }
            else if (gameObject.TryGetComponent<global::BaseDeconstructable>(out var baseDeconstructable))
            {
                return baseDeconstructable.GetMapRoomFunctionality()?.storageContainer?.container;
            }
            else 
            {
                var storage = gameObject.GetComponentInChildren<global::StorageContainer>();
                if (storage)
                {
                    return storage.container;    
                }
            }

            return null;
        }

        public void Dispose()
        {
            this.IsRunning = false;
            this.Queue.Clear();
        }
    }

    public class StorageProcessItem
    {
        public byte OwnerId { get; set; }

        public string ContainerId { get; set; }

        public double FirstCheckTime { get; set; } = 0f;

        public WorldPickupItem WorldPickupItem { get; set; }

        public Action<ItemQueueProcess, Pickupable, GameObject> OnEntitySpawned { get; set; }

        public object CustomProperty { get; set; }

        public bool IsFirstTime()
        {
            return this.FirstCheckTime == 0f;
        }

        public bool IsDelayed()
        {
            return Network.Session.GetWorldTime() > this.FirstCheckTime + Network.Storage.MaxDelay;
        }
    }
}