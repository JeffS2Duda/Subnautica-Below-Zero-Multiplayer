namespace Subnautica.Events.EventArgs
{
    using System;

    public class CrushDamagingEventArgs : EventArgs
    {
        public CrushDamagingEventArgs(string uniqueId, TechType techType, float maxHealth, float damage, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.TechType = techType;
            this.Damage = damage;
            this.MaxHealth = maxHealth;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public TechType TechType { get; set; }

        public float MaxHealth { get; set; }

        public float Damage { get; set; }

        public bool IsAllowed { get; set; }
    }
}
