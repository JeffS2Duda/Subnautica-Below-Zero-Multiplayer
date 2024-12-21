namespace Subnautica.Events.Patches.Events.Player
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using System;

    [HarmonyPatch(typeof(global::PlayerFrozenMixin), nameof(global::PlayerFrozenMixin.Unfreeze))]
    public class Unfreezed
    {
        private static void Prefix(global::PlayerFrozenMixin __instance)
        {
            if (Network.IsMultiplayerActive && __instance.frozen)
            {
                try
                {
                    Handlers.Player.OnUnfreezed();
                }
                catch (Exception e)
                {
                    Log.Error($"Unfreezed.Prefix: {e}\n{e.StackTrace}");
                }
            }
        }
    }
}
