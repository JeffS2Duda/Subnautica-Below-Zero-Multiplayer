namespace Subnautica.Events.EventArgs
{
    using System;

    public class ItemFirstUseAnimationStopedEventArgs : EventArgs
    {
        public ItemFirstUseAnimationStopedEventArgs(TechType techType)
        {
            this.TechType = techType;
        }

        public TechType TechType { get; private set; }
    }
}
