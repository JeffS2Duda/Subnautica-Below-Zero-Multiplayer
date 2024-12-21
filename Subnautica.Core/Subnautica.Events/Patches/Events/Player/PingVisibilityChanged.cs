namespace Subnautica.Events.Patches.Events.Player
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;

    [HarmonyPatch(typeof(global::PingInstance), nameof(global::PingInstance.SetVisible))]
    public class PingVisibilityChanged
    {
        private static void Prefix(global::PingInstance __instance, bool value)
        {
            if (Network.IsMultiplayerActive && __instance.visible != value)
            {
                try
                {
                    PlayerPingVisibilityChangedEventArgs args = new PlayerPingVisibilityChangedEventArgs(__instance.Id, value);

                    Handlers.Player.OnPingVisibilityChanged(args);
                }
                catch (Exception e)
                {
                    Log.Error($"BeaconVisibilityChanged.Prefix: {e}\n{e.StackTrace}");
                }
            }
        }
    }
}
