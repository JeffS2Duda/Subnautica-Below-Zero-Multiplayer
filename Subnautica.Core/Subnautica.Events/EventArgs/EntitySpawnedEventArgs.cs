namespace Subnautica.Events.EventArgs
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using System;
    using UnityEngine;

    public class EntitySpawnedEventArgs : EventArgs
    {
        public EntitySpawnedEventArgs(string uniqueId, GameObject gameObject, string classId, TechType techType, EntitySpawnLevel level, bool isPersistent)
        {
            this.UniqueId = uniqueId;
            this.GameObject = gameObject;
            this.ClassId = classId;
            this.TechType = techType;
            this.Level = level;

            if (uniqueId.IsWorldStreamer())
            {
                this.SlotType = SlotType.WorldStreamer;
            }
            else if (isPersistent && !techType.IsCreature())
            {
                this.SlotType = SlotType.Static;

                Network.StaticEntity.AddStaticEntitySlot(this.UniqueId);
            }
        }

        public string UniqueId { get; set; }

        public GameObject GameObject { get; private set; }

        public string ClassId { get; private set; }

        public EntitySpawnLevel Level { get; private set; }

        public TechType TechType { get; private set; }

        public SlotType SlotType { get; set; }
    }
}
