namespace Subnautica.Events.EventArgs
{
    using System;

    public class LightChangedEventArgs : EventArgs
    {
        public LightChangedEventArgs(string uniqueId, bool isActive, TechType techType)
        {
            this.UniqueId = uniqueId;
            this.IsActive = isActive;
            this.TechType = techType;
        }

        public string UniqueId { get; set; }

        public bool IsActive { get; set; }

        public TechType TechType { get; set; }
    }
}
