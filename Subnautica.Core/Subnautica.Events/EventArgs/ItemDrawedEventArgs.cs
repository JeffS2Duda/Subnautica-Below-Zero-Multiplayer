namespace Subnautica.Events.EventArgs
{
    using System;

    public class ItemDrawedEventArgs : EventArgs
    {
        public ItemDrawedEventArgs(TechType techType, bool isFirstUseAnimationStarted)
        {
            this.TechType = techType;
            this.IsFirstUseAnimationStarted = isFirstUseAnimationStarted;
        }

        public TechType TechType { get; private set; }

        public bool IsFirstUseAnimationStarted { get; private set; }
    }
}
