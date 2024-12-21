namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerExitedInteriorEventArgs : EventArgs
    {
        public PlayerExitedInteriorEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; private set; }
    }
}
