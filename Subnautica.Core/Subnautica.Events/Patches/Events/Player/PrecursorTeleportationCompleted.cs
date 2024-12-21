namespace Subnautica.Events.Patches.Events.Player
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using System;

    [HarmonyPatch(typeof(global::PrecursorTeleporterExitCinematicController), nameof(global::PrecursorTeleporterExitCinematicController.TeleportComplete))]
    public class PrecursorTeleportationCompleted
    {
        private static void Prefix()
        {
            if (Network.IsMultiplayerActive)
            {
                try
                {
                    Handlers.Player.OnPrecursorTeleportationCompleted();
                }
                catch (Exception e)
                {
                    Log.Error($"PrecursorTeleportationCompleted.Prefix: {e}\n{e.StackTrace}");
                }
            }
        }
    }
}