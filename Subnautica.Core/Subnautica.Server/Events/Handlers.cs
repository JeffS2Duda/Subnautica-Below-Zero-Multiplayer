namespace Subnautica.Server.Events
{
    using Subnautica.Server.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class Handlers
    {
        public static event SubnauticaPluginEventHandler<PlayerFullConnectedEventArgs> PlayerFullConnected;

        public static void OnPlayerFullConnected(PlayerFullConnectedEventArgs ev) => PlayerFullConnected.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerDisconnectedEventArgs> PlayerDisconnected;

        public static void OnPlayerDisconnected(PlayerDisconnectedEventArgs ev) => PlayerDisconnected.CustomInvoke(ev);
    }
}
