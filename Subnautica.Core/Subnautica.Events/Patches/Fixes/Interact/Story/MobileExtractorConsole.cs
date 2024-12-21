namespace Subnautica.Events.Patches.Fixes.Interact.Story
{
    using HarmonyLib;

    using Subnautica.API.Enums;
    using Subnautica.API.Features;

    [HarmonyPatch(typeof(global::MobileExtractorConsole), nameof(global::MobileExtractorConsole.OnPointerHover))]
    public class MobileExtractorConsole
    {
        private static bool Prefix(global::MobileExtractorConsole __instance)
        {
            if (Network.IsMultiplayerActive && __instance.GetState(global::MobileExtractorMachine.main) == global::MobileExtractorConsole.State.Ready)
            {
                return Network.Story.ShowWaitingForPlayersMessage(StoryCinematicType.StoryFrozenCreatureInject);
            }

            return true;
        }
    }
}
