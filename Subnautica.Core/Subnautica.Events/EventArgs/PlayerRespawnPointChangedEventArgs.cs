namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerRespawnPointChangedEventArgs : EventArgs
    {
        public PlayerRespawnPointChangedEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; set; }
    }
}
