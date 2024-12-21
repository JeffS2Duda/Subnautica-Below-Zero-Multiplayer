namespace Subnautica.Events.EventArgs
{
    using Subnautica.API.Enums;
    using System;
    using System.Collections.Generic;

    public class PlayerAnimationChangedEventArgs : EventArgs
    {
        public PlayerAnimationChangedEventArgs(Dictionary<PlayerAnimationType, bool> animations)
        {
            this.Animations = animations;
        }

        public Dictionary<PlayerAnimationType, bool> Animations { get; private set; } = new Dictionary<PlayerAnimationType, bool>();
    }
}
