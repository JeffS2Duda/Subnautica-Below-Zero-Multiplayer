namespace Subnautica.Events.Patches.Events.Player
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using System;
    using System.Collections;

    [HarmonyPatch(typeof(global::Player), nameof(global::Player.ResetPlayerOnDeath))]
    public class Spawned
    {
        private static IEnumerator Postfix(IEnumerator values, global::Player __instance)
        {
            yield return values;

            if (Network.IsMultiplayerActive)
            {
                try
                {
                    Handlers.Player.OnSpawned();
                }
                catch (Exception e)
                {
                    Log.Error($"Spawned.Postfix: {e}\n{e.StackTrace}");
                }
            }
        }
    }
}
