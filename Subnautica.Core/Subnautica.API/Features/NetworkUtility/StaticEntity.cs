namespace Subnautica.API.Features.NetworkUtility
{
    using System.Collections.Generic;

    using Subnautica.Network.Core.Components;

    public class StaticEntity
    {
        public Dictionary<string, NetworkWorldEntityComponent> StaticEntities { get; private set; } = new Dictionary<string, NetworkWorldEntityComponent>();

        public bool IsStaticEntity(string uniqueId)
        {
            return this.StaticEntities.ContainsKey(uniqueId);
        }

        public NetworkWorldEntityComponent GetEntity(string uniqueId)
        {
            if (this.StaticEntities.TryGetValue(uniqueId, out var entity))
            {
                return entity;
            }

            return null;
        }

        public T GetEntity<T>(string uniqueId)
        {
            if (this.StaticEntities.TryGetValue(uniqueId, out var entity) && entity != null)
            {
                return entity.GetComponent<T>();
            }

            return default(T);
        }

        public bool IsRestricted(string uniqueId)
        {
            if (this.StaticEntities.TryGetValue(uniqueId, out var entity) && entity != null)
            {
                return !entity.IsSpawnable;
            }

            return false;
        }

        public void AddStaticEntity(NetworkWorldEntityComponent entity)
        {
            if (entity != null)
            {
                this.StaticEntities[entity.UniqueId] = entity;
            }
        }

        public void AddStaticEntitySlot(string uniqueId)
        {
            if (!this.StaticEntities.ContainsKey(uniqueId))
            {
                this.StaticEntities[uniqueId] = null;
            }
        }

        public void SetStaticEntities(Dictionary<string, NetworkWorldEntityComponent> entities)
        {
            this.StaticEntities = entities;
        }

        public void Dispose()
        {
            this.StaticEntities.Clear();
        }
    }
}