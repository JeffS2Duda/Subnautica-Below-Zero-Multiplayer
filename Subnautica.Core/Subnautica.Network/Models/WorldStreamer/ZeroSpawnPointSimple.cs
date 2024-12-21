namespace Subnautica.Network.Models.WorldStreamer
{
    using MessagePack;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Structures;

    using UnityEngine;

    [MessagePackObject]
    public class ZeroSpawnPointSimple
    {
        [Key(0)]
        public int SlotId { get; set; }

        [Key(1)]
        public string ClassId { get; set; }

        [Key(2)]
        public float NextRespawnTime { get; set; } = 0;

        [Key(3)]
        public float Health { get; set; } = -1;

        [IgnoreMember]
        public TechType TechType { get; set; }

        [IgnoreMember]
        public bool IsActive { get; set; }

        [IgnoreMember]
        public bool IsDead
        { 
            get 
            {
                return this.Health == 0;
            } 
        }

        public ZeroSpawnPointSimple()
        {

        }

        public ZeroSpawnPointSimple(int slotId, string classId, float nextRespawnTime)
        {
            this.SlotId          = slotId;
            this.ClassId         = classId;
            this.NextRespawnTime = nextRespawnTime;
        }

        public void SetHealth(float health)
        {
            this.Health = health;
        }

        public bool TakeDamage(float damage, float maxHealth)
        {
            if (this.Health == -1)
            {
                this.Health = maxHealth;
            }

            if (this.Health == 0)
            {
                return false;
            }

            this.Health = Mathf.Max(0, this.Health - damage);
            return true;
        }

        public bool Kill()
        {
            if (this.Health == 0)
            {
                return false;
            }

            this.Health = 0;
            return true;
        }

        public bool IsRespawnable(float currentTime)
        {
            return this.NextRespawnTime != -1 && (this.NextRespawnTime == 0 || currentTime >= this.NextRespawnTime);
        }

        public float GetNextRespawnTime(float currentTime)
        {
            var duration = this.TechType.GetRespawnDuration();
            if (duration == -1f)
            {
                return -1f;
            }

            return currentTime + duration;
        }

        public void DisableRespawn()
        {
            this.NextRespawnTime = -1;
        }

        public ZeroSpawnPoint ConvertToZeroSpawnPoint()
        {
            return new ZeroSpawnPoint()
            {
                IsActive        = true,
                SlotId          = this.SlotId,
                ClassId         = this.ClassId,
                NextRespawnTime = this.NextRespawnTime,
            };
        }
    }
}
