namespace Subnautica.Events.EventArgs
{
    using System;

    public class WeldingEventArgs : EventArgs
    {
        public WeldingEventArgs(string uniqueId, TechType techType, float health, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.TechType = techType;
            this.Health = health;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public TechType TechType { get; set; }

        public float Health { get; set; }

        public bool IsAllowed { get; set; }
    }
}
