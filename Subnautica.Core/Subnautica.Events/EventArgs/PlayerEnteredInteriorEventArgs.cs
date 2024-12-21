namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerEnteredInteriorEventArgs : EventArgs
    {
        public PlayerEnteredInteriorEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; private set; }
    }
}
