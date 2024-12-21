namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerBaseEnteredEventArgs : EventArgs
    {
        public PlayerBaseEnteredEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; }
    }
}
