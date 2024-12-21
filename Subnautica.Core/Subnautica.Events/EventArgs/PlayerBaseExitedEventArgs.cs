namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerBaseExitedEventArgs : EventArgs
    {
        public PlayerBaseExitedEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; }
    }
}
