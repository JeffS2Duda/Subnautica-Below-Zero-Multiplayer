namespace Subnautica.Events.Patches.Events.Player
{
    using HarmonyLib;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;

    [HarmonyPatch(typeof(global::Player), nameof(global::Player.EnterInterior))]
    public static class EnteredInterior
    {
        private static void Prefix(global::Player __instance, IInteriorSpace interior)
        {
            if (Network.IsMultiplayerActive)
            {
                if (interior == __instance.currentInterior && __instance.currentWaterPark == null)
                {
                    return;
                }

                if (interior is SubRoot)
                {
                    return;
                }

                if (interior.GetGameObject().TryGetComponent<global::SeaTruckSegment>(out var seaTruckSegment))
                {
                    var expansionManager = seaTruckSegment.GetFirstSegment()?.GetDockedMoonpoolExpansion();
                    if (expansionManager)
                    {
                        return;
                    }
                }

                try
                {
                    PlayerEnteredInteriorEventArgs args = new PlayerEnteredInteriorEventArgs(interior.GetGameObject().GetIdentityId());

                    Handlers.Player.OnEnteredInterior(args);
                }
                catch (Exception e)
                {
                    Log.Error($"EnteredInterior.Prefix: {e}\n{e.StackTrace}");
                }
            }
        }
    }
}
