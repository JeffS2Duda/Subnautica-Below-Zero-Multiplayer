namespace Subnautica.Events.EventArgs
{
    using System;

    public class CreatureAnimationChangedEventArgs : EventArgs
    {
        public CreatureAnimationChangedEventArgs(ushort creatureId, byte animationId, byte result)
        {
            this.CreatureId  = creatureId;
            this.AnimationId = animationId;
            this.Result      = result;
        }

        public ushort CreatureId { get; private set; }

        public byte AnimationId { get; private set; }

        public byte Result { get; private set; }
    }
}
