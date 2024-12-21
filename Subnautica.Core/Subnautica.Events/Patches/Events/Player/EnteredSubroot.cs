namespace Subnautica.Events.Patches.Events.Player
{
    using HarmonyLib;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;

    [HarmonyPatch(typeof(global::SubRoot), nameof(global::SubRoot.OnPlayerEntered))]
    public static class EnteredSubroot
    {
        private static void Prefix(global::SubRoot __instance)
        {
            if (Network.IsMultiplayerActive && __instance.isBase)
            {
                try
                {
                    PlayerBaseEnteredEventArgs args = new PlayerBaseEnteredEventArgs(__instance.gameObject.GetIdentityId());

                    Handlers.Player.OnPlayerBaseEntered(args);
                }
                catch (Exception e)
                {
                    Log.Error($"ExitedSubroot.Prefix: {e}\n{e.StackTrace}");
                }
            }
        }
    }
}
