namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerPingColorChangedEventArgs : EventArgs
    {
        public PlayerPingColorChangedEventArgs(string uniqueId, int colorIndex)
        {
            this.UniqueId = uniqueId;
            this.ColorIndex = colorIndex;
        }

        public string UniqueId { get; private set; }

        public int ColorIndex { get; private set; }
    }
}
