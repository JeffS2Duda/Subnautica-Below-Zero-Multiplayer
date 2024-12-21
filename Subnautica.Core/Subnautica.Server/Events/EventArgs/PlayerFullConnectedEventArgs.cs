namespace Subnautica.Server.Events.EventArgs
{
    using System;

    using Subnautica.Server.Core;

    public class PlayerFullConnectedEventArgs : EventArgs
    {
        public PlayerFullConnectedEventArgs(AuthorizationProfile player)
        {
            this.Player = player;
        }

        public AuthorizationProfile Player { get; set; }
    }
}
