namespace Subnautica.Events.EventArgs
{
    using System;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;

    using UnityEngine;

    public class TakeDamagingEventArgs : EventArgs
    {
        public TakeDamagingEventArgs(global::LiveMixin liveMixin, TechType techType, float damage, float oldHealth, float maxHealth, float newHealth, DamageType damageType, bool isDestroyable, GameObject dealer, bool isAllowed = true)
        {
            this.LiveMixin     = liveMixin;
            this.UniqueId      = Network.Identifier.GetIdentityId(liveMixin.gameObject, false);
            this.Dealer        = dealer;
            this.DealerId      = dealer != null ? Network.Identifier.GetIdentityId(dealer.gameObject, false) : null;
            this.TechType      = techType;
            this.Damage        = damage;
            this.OldHealth     = oldHealth;
            this.MaxHealth     = maxHealth;
            this.NewHealth     = newHealth;
            this.DamageType    = damageType;
            this.IsDead        = newHealth <= 0f;
            this.IsDestroyable = isDestroyable;
            this.IsAllowed     = isAllowed;

            if (this.UniqueId.IsNotNull())
            {
                this.IsStaticWorldEntity = Network.StaticEntity.IsStaticEntity(this.UniqueId);
            }
        }

        public global::LiveMixin LiveMixin { get; set; }

        public string UniqueId { get; set; }

        public GameObject Dealer { get; set; }

        public string DealerId { get; set; }

        public TechType TechType { get; set; }

        public float Damage { get; set; }

        public float OldHealth { get; set; }

        public float MaxHealth { get; set; }

        public float NewHealth { get; set; }

        public DamageType DamageType { get; set; }

        public bool IsDestroyable { get; private set; }

        public bool IsStaticWorldEntity { get; private set; }

        public bool IsDead { get; set; }

        public bool IsAllowed { get; set; }
    }
}
