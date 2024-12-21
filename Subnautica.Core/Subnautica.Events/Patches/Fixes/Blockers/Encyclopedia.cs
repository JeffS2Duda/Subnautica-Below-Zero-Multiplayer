namespace Subnautica.Events.Patches.Fixes.Encyclopedia
{
    using HarmonyLib;

    using Subnautica.API.Enums;
    using Subnautica.API.Features;

    [HarmonyPatch(typeof(PDAEncyclopedia), nameof(PDAEncyclopedia.Initialize))]
    public static class Encyclopedia
    {
        private static EventBlocker Blocker = null;

        private static void Prefix(PDAData pdaData)
        {
            if (Network.IsMultiplayerActive)
            {
                Blocker = EventBlocker.Create(ProcessType.EncyclopediaAdded);
            }
        }

        private static void Postfix(PDAData pdaData)
        {
            if (Blocker != null)
            {
                Blocker.Dispose();
            }
        }
    }
}
