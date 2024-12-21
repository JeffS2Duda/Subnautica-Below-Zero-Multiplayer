namespace Subnautica.Events.Patches.Events.Player
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;

    [HarmonyPatch(typeof(global::PingInstance), nameof(global::PingInstance.SetColor))]
    public class PingColorChanged
    {
        private static void Prefix(global::PingInstance __instance, int index)
        {
            if (Network.IsMultiplayerActive)
            {
                if (index >= PingManager.colorOptions.Length)
                {
                    index = 0;
                }

                if (__instance.colorIndex != index)
                {
                    try
                    {
                        PlayerPingColorChangedEventArgs args = new PlayerPingColorChangedEventArgs(__instance._id, index);

                        Handlers.Player.OnPingColorChanged(args);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"BeaconColorChanged.Prefix: {e}\n{e.StackTrace}");
                    }
                }
            }
        }
    }
}
