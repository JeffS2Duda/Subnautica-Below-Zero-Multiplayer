namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class CreatureAttackLastTargetStartingEventArgs : EventArgs
    {
        public CreatureAttackLastTargetStartingEventArgs(global::Creature creature, string uniqueId, GameObject target, float minAttackDuration, float maxAttackDuration, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Creature = creature;
            this.Target = target;
            this.MinAttackDuration = minAttackDuration;
            this.MaxAttackDuration = maxAttackDuration;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public global::Creature Creature { get; set; }

        public GameObject Target { get; set; }

        public float MinAttackDuration { get; set; }

        public float MaxAttackDuration { get; set; }

        public bool IsAllowed { get; set; }
    }
}
