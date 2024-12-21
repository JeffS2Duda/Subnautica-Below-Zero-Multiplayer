namespace Subnautica.Network.Structures
{
    using MessagePack;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;

    using UnityEngine;

    [MessagePackObject]
    public class ZeroLastTarget
    {
        [Key(0)]
        public string TargetId { get; set; }

        [Key(1)]
        public TechType Type { get; set; }

        [Key(2)]
        public bool IsDead { get; set; }

        public ZeroLastTarget()
        {

        }

        public ZeroLastTarget(string targetId, TechType type)
        {
            this.TargetId = targetId;
            this.Type = type;
        }

        public void Kill()
        {
            this.IsDead = true;
        }

        public GameObject GetGameObject(bool supressMessage = false)
        {
            return Network.Identifier.GetGameObject(this.TargetId, supressMessage);
        }

        public bool IsPlayer()
        {
            return this.Type.IsPlayer();
        }

        public bool IsCreature()
        {
            return this.Type.IsCreature();
        }

        public bool IsVehicle()
        {
            return this.Type.IsVehicle();
        }

        public bool IsSeatruck()
        {
            return this.Type == TechType.SeaTruck;
        }

        public bool IsExosuit()
        {
            return this.Type == TechType.Exosuit;
        }
    }
}
