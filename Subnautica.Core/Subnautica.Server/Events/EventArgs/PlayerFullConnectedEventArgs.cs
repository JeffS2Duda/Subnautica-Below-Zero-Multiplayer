namespace Subnautica.Server.Events.EventArgs
{
    using Subnautica.Server.Core;
    using System;

    public class PlayerFullConnectedEventArgs : EventArgs
    {
        public PlayerFullConnectedEventArgs(AuthorizationProfile player)
        {
            this.Player = player;
        }

        public AuthorizationProfile Player { get; set; }
    }
}
