namespace Subnautica.Events.Patches.Fixes.Interact
{
    using HarmonyLib;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;

    [HarmonyPatch]
    public class SeaTruckDockingModule
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::SeaTruckDockingBay), nameof(global::SeaTruckDockingBay.OnHoverExosuit))]
        private static bool OnHoverExosuit(global::SeaTruckDockingBay __instance)
        {
            if (!Network.IsMultiplayerActive || __instance.dockedObject == null)
            {
                return true;
            }

            if (Network.HandTarget.IsBlocked(__instance.dockedObject.gameObject.GetIdentityId()))
            {
                Interact.ShowUseDenyMessage();
                return false;
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::SeaTruckDockingBay), nameof(global::SeaTruckDockingBay.OnHoverEjectDocked))]
        private static bool OnHoverEjectDocked(global::SeaTruckDockingBay __instance)
        {
            if (!Network.IsMultiplayerActive || __instance.dockedObject == null)
            {
                return true;
            }

            if (Network.HandTarget.IsBlocked(__instance.dockedObject.gameObject.GetIdentityId()))
            {
                Interact.ShowUseDenyMessage();
                return false;
            }

            return true;
        }
    }
}
