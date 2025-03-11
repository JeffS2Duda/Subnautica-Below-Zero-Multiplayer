namespace Subnautica.Events.Patches.Fixes.Interact
{
    using HarmonyLib;

    using Subnautica.API.Features;
    using UnityEngine;

    [HarmonyPatch]
    public class Pickupable
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::Pickupable), nameof(global::Pickupable.OnHandHover))]
        private static bool OnHandHover(global::Pickupable __instance, GUIHand hand)
        {
            if (!Network.IsMultiplayerActive)
            {
                return true;
            }

            if (!hand.IsFreeToInteract())
            {
                return false;
            }

            if (Interact.IsBlocked(Network.Identifier.GetIdentityId(__instance.gameObject)))
            {
                Interact.ShowUseDenyMessage();
                return false;
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::Pickupable), nameof(global::Pickupable.OnHandClick))]
        private static bool OnHandClick(global::Pickupable __instance, GUIHand hand)
        {
            if (!Network.IsMultiplayerActive)
            {
                return true;
            }

            if (!hand.IsFreeToInteract() || !__instance.AllowedToPickUp())
            {
                return false;
            }

            if (Interact.IsBlocked(Network.Identifier.GetIdentityId(__instance.gameObject)))
            {
                Interact.ShowUseDenyMessage();
                return false;
            }
            if (__instance.pickupCinematic && __instance.pickupCinematic.enabled)
            {
                __instance.quickSlot = Inventory.main.quickSlots.activeSlot;
                if (!Inventory.main.ReturnHeld())
                    return false;
                __instance.pickupCinematic.StartCinematicMode(global::Player.main);
            }
            else if (!Inventory.Get().Pickup(__instance))
                ErrorMessage.AddWarning(Language.main.Get("InventoryFull"));
            else
                global::Player.main.PlayGrab();
            return true;
        }
    }
}
