namespace Subnautica.Events.EventArgs
{
    using System;

    public class CreatureAttackLastTargetStoppedEventArgs : EventArgs
    {
        public CreatureAttackLastTargetStoppedEventArgs(global::Creature creature, string uniqueId, bool isAttackAnimationActive)
        {
            this.UniqueId = uniqueId;
            this.Creature = creature;
            this.IsAttackAnimationActive = isAttackAnimationActive;
        }

        public string UniqueId { get; set; }

        public global::Creature Creature { get; set; }

        public bool IsAttackAnimationActive { get; set; }
    }
}
