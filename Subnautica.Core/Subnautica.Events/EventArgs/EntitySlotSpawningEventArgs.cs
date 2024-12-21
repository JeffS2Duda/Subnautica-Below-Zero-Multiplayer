namespace Subnautica.Events.EventArgs
{
    using System;

    using Subnautica.API.Enums;
    using Subnautica.API.Features;

    public class EntitySlotSpawningEventArgs : EventArgs
    {
        public EntitySlotSpawningEventArgs(int slotId, bool isAllowed = true)
        {
            this.SlotId    = slotId;
            this.IsAllowed = isAllowed;
        }

        public int SlotId { get; set; }

        public string ClassId { get; set; }

        public bool IsAllowed { get; set; }
    }
}
