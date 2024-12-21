namespace Subnautica.API.Features.Helper
{
    using Subnautica.Network.Structures;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class ItemQueueProcess
    {
        public bool IsSpawning { get; set; } = false;

        public bool IsProcess { get; set; } = false;

        public TechType TechType { get; set; } = TechType.None;

        public string ItemId { get; set; }

        public byte[] Item { get; set; }

        public ItemsContainer Container { get; set; }

        public ZeroTransform Transform { get; set; }

        public string SlotId { get; set; }

        public Equipment Equipment { get; set; }

        public Pickupable Pickupable { get; set; }

        public ItemQueueAction Action { get; set; } = new ItemQueueAction();

        public void Dipose()
        {
            this.IsSpawning = false;
            this.IsProcess = false;
            this.TechType = TechType.None;
            this.Item = null;
            this.Container = null;
            this.Equipment = null;
            this.Pickupable = null;
        }
    }

    public class ItemQueueAction
    {
        private List<GenericProperty> Properties = new List<GenericProperty>();

        public Func<ItemQueueProcess, bool> OnEntitySpawning { get; set; }

        public Action<ItemQueueProcess, Pickupable, GameObject> OnEntitySpawned { get; set; }

        public Action<ItemQueueProcess> OnProcessCompleted { get; set; }

        public Func<ItemQueueProcess, IEnumerator> OnProcessCompletedAsync { get; set; }

        public Action<ItemQueueProcess> OnEntityRemoved { get; set; }

        public ItemQueueAction(Func<ItemQueueProcess, bool> entitySpawning)
        {
            this.OnEntitySpawning = entitySpawning;
        }

        public ItemQueueAction(Action<ItemQueueProcess, Pickupable, GameObject> entitySpawned = null)
        {
            this.OnEntitySpawned = entitySpawned;
        }

        public ItemQueueAction(Func<ItemQueueProcess, bool> entitySpawning, Action<ItemQueueProcess, Pickupable, GameObject> entitySpawned)
        {
            this.OnEntitySpawning = entitySpawning;
            this.OnEntitySpawned = entitySpawned;
        }

        public void RegisterProperty(string key, object value)
        {
            this.Properties.Add(new GenericProperty(key, value));
        }

        public T GetProperty<T>(string key)
        {
            var property = this.Properties.Where(q => q.Key == key).FirstOrDefault();
            if (property == null || property.Value == null)
            {
                return default(T);
            }

            return (T)property.Value;
        }
    }
}
