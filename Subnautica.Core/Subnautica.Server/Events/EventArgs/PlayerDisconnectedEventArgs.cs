namespace Subnautica.Server.Events.EventArgs
{
    using System;

    using Subnautica.Server.Core;

    public class PlayerDisconnectedEventArgs : EventArgs
    {
        public PlayerDisconnectedEventArgs(AuthorizationProfile player)
        {
            this.Player = player;
        }

        public AuthorizationProfile Player { get; set; }
    }
}
