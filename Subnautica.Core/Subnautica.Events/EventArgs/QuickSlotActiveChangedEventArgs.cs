namespace Subnautica.Events.EventArgs
{
    using System;

    public class QuickSlotActiveChangedEventArgs : EventArgs
    {
        public QuickSlotActiveChangedEventArgs(int slotId)
        {
            this.SlotId = slotId;
        }

        public int SlotId { get; private set; }
    }
}
