namespace Subnautica.Network.Models.WorldStreamer
{
    using MessagePack;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.API.MonoBehaviours;
    using Subnautica.Network.Structures;

    using UnityEngine;

    using static EntitySlotData;

    [MessagePackObject]
    public class ZeroSpawnPoint
    {
        [Key(0)]
        public bool IsActive { get; set; }

        [Key(1)]
        public int SlotId { get; set; }

        [Key(2)]
        public BiomeType BiomeType { get; set; }

        [Key(3)]
        public float Density { get; set; }

        [Key(4)]
        public EntitySlotType SlotType { get; set; }

        [Key(5)]
        public ZeroVector3 LeashPosition { get; set; }

        [Key(6)]
        public ZeroQuaternion LeashRotation { get; set; }

        [IgnoreMember]
        public string ClassId { get; set; }

        [IgnoreMember]
        public TechType TechType { get; set; }

        [IgnoreMember]
        public GameObject GameObject { get; set; }

        [IgnoreMember]
        public float NextRespawnTime { get; set; }

        [IgnoreMember]
        public float Health { get; set; } = -1f;

        public ZeroSpawnPoint Clone()
        {
            return new ZeroSpawnPoint()
            {
                IsActive        = this.IsActive, 
                SlotId          = this.SlotId, 
                BiomeType       = this.BiomeType, 
                Density         = this.Density, 
                SlotType        = this.SlotType,
                LeashPosition   = this.LeashPosition,
                LeashRotation   = this.LeashRotation,
            };
        }

        public void SetActive(bool isActive, TechType techType, string classId)
        {
            this.IsActive = isActive;
            this.TechType = techType;
            this.ClassId  = classId;
        }

        public void SetHealth(float health)
        {
            this.Health = health;
        }

        public float GetDensity()
        {
            return this.Density;
        }

        public bool IsTypeAllowed(EntitySlot.Type slotType)
        {
            return EntitySlotData.IsTypeAllowed(this.SlotType, slotType);
        }

        public SpawnPointComponent GetComponent()
        {
            return Network.Identifier.GetComponentByGameObject<SpawnPointComponent>(this.SlotId.ToWorldStreamerId(), true);
        }

        public bool IsRespawnActive()
        {
            return this.IsActive && this.NextRespawnTime != -1;
        }

        public bool IsRespawnable(float currentTime)
        {
            return this.IsRespawnActive() && (this.NextRespawnTime == 0 || currentTime >= this.NextRespawnTime);
        }
    }
}
