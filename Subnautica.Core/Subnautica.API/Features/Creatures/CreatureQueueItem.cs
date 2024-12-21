namespace Subnautica.API.Features.Creatures
{
    using Subnautica.API.Features.Helper;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CreatureQueueItem
    {
        public ushort CreatureId { get; set; }

        public bool IsSpawn { get; set; }

        public bool IsProcess { get; set; }

        public bool IsChangeOWS { get; set; }

        public bool IsDeath { get; set; }

        public CreatureQueueAction Action { get; set; }
    }

    public class CreatureQueueAction
    {
        private List<GenericProperty> Properties = new List<GenericProperty>();

        public Action<MultiplayerCreature, CreatureQueueItem> OnProcessCompleted { get; set; }

        public void RegisterProperty(string key, object value)
        {
            this.Properties.Add(new GenericProperty(key, value));
        }

        public T GetProperty<T>(string key)
        {
            var property = this.Properties.Where(q => q.Key == key).FirstOrDefault();
            if (property == null)
            {
                return default(T);
            }

            return (T)property.GetValue<T>();
        }
    }
}