namespace Subnautica.Events.Patches.Identity.World
{
    using HarmonyLib;

    using Subnautica.API.Features;

    [HarmonyPatch(typeof(global::UniqueIdentifier), nameof(global::UniqueIdentifier.EnsureGuid))]
    public static class UniqueIdentifier
    {
        private static bool Prefix(ref string __result, string guid)
        {
            if (!Network.IsMultiplayerActive)
            {
                return true;
            }

            if (string.IsNullOrEmpty(guid))
            {
                __result = Network.Identifier.GenerateUniqueId();
            }
            else
            {
                __result = guid;
            }

            return false;
        }
    }
}
