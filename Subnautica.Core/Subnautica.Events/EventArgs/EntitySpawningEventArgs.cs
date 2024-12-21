namespace Subnautica.Events.EventArgs
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using System;

    public class EntitySpawningEventArgs : EventArgs
    {
        public EntitySpawningEventArgs(string uniqueId, string classId, TechType techType, EntitySpawnLevel level, bool isPersistent, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.ClassId = classId;
            this.TechType = techType;
            this.Level = level;
            this.IsAllowed = isAllowed;

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

        public string ClassId { get; private set; }

        public EntitySpawnLevel Level { get; private set; }

        public TechType TechType { get; private set; }

        public SlotType SlotType { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
