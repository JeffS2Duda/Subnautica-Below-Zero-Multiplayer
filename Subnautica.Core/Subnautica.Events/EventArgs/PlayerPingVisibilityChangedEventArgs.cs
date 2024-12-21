namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerPingVisibilityChangedEventArgs : EventArgs
    {
        public PlayerPingVisibilityChangedEventArgs(string uniqueId, bool isVisible)
        {
            this.UniqueId = uniqueId;
            this.IsVisible = isVisible;
        }

        public string UniqueId { get; private set; }

        public bool IsVisible { get; private set; }
    }
}
