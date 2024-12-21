namespace Subnautica.Events.EventArgs
{
    using System;

    public class ItemActionStartedEventArgs : EventArgs
    {
        public ItemActionStartedEventArgs(TechType techType, bool isFirstUseAnimationStarted)
        {
            this.TechType = techType;
            this.IsFirstUseAnimationStarted = isFirstUseAnimationStarted;
        }

        public TechType TechType { get; private set; }

        public bool IsFirstUseAnimationStarted { get; private set; }
    }
}
