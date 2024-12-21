namespace Subnautica.Events.Patches.Fixes.Interact.Story
{
    using HarmonyLib;

    using Subnautica.API.Enums;
    using Subnautica.API.Features;

    [HarmonyPatch(typeof(global::MobileExtractorMachine), nameof(global::MobileExtractorMachine.OnHoverCanister))]
    public class MobileExtractorMachine
    {
        private static bool Prefix(global::MobileExtractorMachine __instance)
        {
            if (Network.IsMultiplayerActive && !__instance.hasSample && Inventory.main.GetPickupCount(__instance.sampleTechType) > 0)
            {
                return Network.Story.ShowWaitingForPlayersMessage(StoryCinematicType.StoryFrozenCreatureSample);
            }

            return true;
        }
    }
}
