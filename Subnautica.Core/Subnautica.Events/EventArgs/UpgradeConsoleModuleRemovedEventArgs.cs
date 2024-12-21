namespace Subnautica.Events.EventArgs
{
    using System;

    public class UpgradeConsoleModuleRemovedEventArgs : EventArgs
    {
        public UpgradeConsoleModuleRemovedEventArgs(string uniqueId, string slotId, string itemId, TechType moduleType)
        {
            this.UniqueId = uniqueId;
            this.SlotId = slotId;
            this.ItemId = itemId;
            this.ModuleType = moduleType;
        }

        public string UniqueId { get; set; }

        public string SlotId { get; set; }

        public string ItemId { get; set; }

        public TechType ModuleType { get; set; }
    }
}
