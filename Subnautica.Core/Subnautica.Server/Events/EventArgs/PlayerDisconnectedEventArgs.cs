namespace Subnautica.Server.Events.EventArgs
{
    using Subnautica.Server.Core;
    using System;

    public class PlayerDisconnectedEventArgs : EventArgs
    {
        public PlayerDisconnectedEventArgs(AuthorizationProfile player)
        {
            this.Player = player;
        }

        public AuthorizationProfile Player { get; set; }
    }
}
