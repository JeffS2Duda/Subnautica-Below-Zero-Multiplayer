namespace Subnautica.Events.Patches.Events.Player
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;

    [HarmonyPatch(typeof(global::Player), nameof(global::Player.OnKill))]
    public class Dead
    {
        private static void Postfix(global::Player __instance, DamageType damageType)
        {
            if (Network.IsMultiplayerActive)
            {
                try
                {
                    PlayerDeadEventArgs args = new PlayerDeadEventArgs(damageType);

                    Handlers.Player.OnDead(args);
                }
                catch (Exception e)
                {
                    Log.Error($"PlayerDead.Postfix: {e}\n{e.StackTrace}");
                }
            }
        }
    }
}
