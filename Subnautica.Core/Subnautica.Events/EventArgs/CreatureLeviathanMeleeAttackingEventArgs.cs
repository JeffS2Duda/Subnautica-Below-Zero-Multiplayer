namespace Subnautica.Events.EventArgs
{
    using Subnautica.API.Extensions;
    using System;
    using UnityEngine;

    public class CreatureLeviathanMeleeAttackingEventArgs : EventArgs
    {
        public CreatureLeviathanMeleeAttackingEventArgs(global::LeviathanMeleeAttack instance, GameObject target, bool isAllowed = true)
        {
            this.Instance = instance;
            this.UniqueId = instance.creature.gameObject.GetIdentityId();
            this.BiteDamage = instance.biteDamage;
            this.Target = target;
            this.TargetId = target.GetIdentityId();
            this.TargetType = target.GetTechType();
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public global::LeviathanMeleeAttack Instance { get; set; }

        public GameObject Target { get; set; }

        public string TargetId { get; set; }

        public TechType TargetType { get; set; }

        public float BiteDamage { get; set; }

        public bool IsAllowed { get; set; }
    }
}
