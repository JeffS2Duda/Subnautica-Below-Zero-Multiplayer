namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerDeadEventArgs : EventArgs
    {
        public PlayerDeadEventArgs(DamageType damageType)
        {
            this.DamageType = damageType;
        }

        public DamageType DamageType { get; set; }
    }
}
