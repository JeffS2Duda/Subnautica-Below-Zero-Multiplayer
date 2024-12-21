namespace Subnautica.Events.EventArgs
{
    using Subnautica.API.Extensions;
    using System;
    using UnityEngine;

    public class CreatureMeleeAttackingEventArgs : EventArgs
    {
        public CreatureMeleeAttackingEventArgs(global::MeleeAttack instance, GameObject target, bool isAllowed = true)
        {
            this.Instance = instance;
            this.UniqueId = instance.creature.gameObject.GetIdentityId();
            this.BiteDamage = instance.GetBiteDamage(target);
            this.Target = target;
            this.TargetId = target.GetIdentityId();
            this.TargetType = target.GetTechType();
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public global::MeleeAttack Instance { get; set; }

        public GameObject Target { get; set; }

        public string TargetId { get; set; }

        public TechType TargetType { get; set; }

        public float BiteDamage { get; set; }

        public bool IsAllowed { get; set; }
    }
}
