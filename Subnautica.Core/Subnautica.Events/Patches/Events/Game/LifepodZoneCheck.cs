namespace Subnautica.Events.Patches.Events.Game
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;

    [HarmonyPatch(typeof(global::SupplyDropManager), nameof(global::SupplyDropManager.CheckForDrops))]
    public static class LifepodZoneCheck
    {
        private static bool Prefix(string goalCompleted)
        {
            if (!Network.IsMultiplayerActive || BelowZeroEndGame.isActive)
            {
                return true;
            }

            try
            {
                LifepodZoneCheckEventArgs args = new LifepodZoneCheckEventArgs(goalCompleted);

                Handlers.Game.OnLifepodZoneCheck(args);

                return args.IsAllowed;
            }
            catch (Exception e)
            {
                Log.Error($"LifepodZoneCheck.Prefix: {e}\n{e.StackTrace}");
                return false;
            }
        }
    }
}