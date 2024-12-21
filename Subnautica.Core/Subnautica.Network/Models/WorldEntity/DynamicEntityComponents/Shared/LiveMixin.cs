namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared
{
    using MessagePack;

    using UnityEngine;

    [MessagePackObject]
    public class LiveMixin
    {
        [Key(0)]
        public float Health { get; set; }

        [Key(1)]
        public float MaxHealth { get; set; }

        [IgnoreMember]
        public bool IsDead
        {
            get
            {
                return this.Health == 0;
            }
        }

        [IgnoreMember]
        public bool IsHealthFull
        {
            get
            {
                return this.Health == this.MaxHealth;
            }
        }

        [IgnoreMember]
        public float MaxDamagePercent { get; set; } = 0.16f;

        public LiveMixin()
        {

        }

        public LiveMixin(float health, float maxHealth)
        {
            this.Health    = health;
            this.MaxHealth = maxHealth;
        }

        public bool AddHealth(float health)
        {
            if (this.IsHealthFull)
            {
                return false;
            }

            this.Health = Mathf.Min(this.MaxHealth, this.Health + health);
            return true;
        }

        public void ResetHealth()
        {
            this.Health = this.MaxHealth;
        }

        public void SetHealth(float health)
        {
            this.Health = health;
        }

        public float CalculateDamage(float damage, DamageType damageType)
        {
            if (damageType == DamageType.Starve)
            {
                return damage;
            }

            float maxDamage = this.MaxHealth * this.MaxDamagePercent;

            if (damage > maxDamage)
            {
                damage = maxDamage;
            }

            return damage;
        }

        public bool TakeDamage(float damage)
        {
            if (this.IsDead || damage <= 0.0f)
            {
                return false;
            }

            this.Health = Mathf.Max(0, this.Health - damage);
            return true;
        }

        public bool Kill()
        {
            if (this.IsDead)
            {
                return false;
            }

            this.Health = 0;
            return true;
        }
    }
}
