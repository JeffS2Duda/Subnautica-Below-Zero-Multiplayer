namespace Subnautica.Events.Patches.Events.Player
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using System;

    [HarmonyPatch(typeof(global::uGUI_PlayerSleep), nameof(global::uGUI_PlayerSleep.BeginFadeOut))]
    public static class SleepScreenStopingStarted
    {
        private static bool Prefix(global::uGUI_PlayerSleep __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                try
                {
                    Handlers.Player.OnSleepScreenStopingStarted();
                }
                catch (Exception e)
                {
                    Log.Error($"SleepScreenStopingStarted.Prefix: {e}\n{e.StackTrace}");
                }
            }

            return true;
        }
    }
}
