namespace Subnautica.Events.Patches.Fixes.Creatures
{
    using HarmonyLib;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;

    [HarmonyPatch(typeof(global::Creature), nameof(global::Creature.InitializeOnce))]
    public class Creature
    {
        public static bool Prefix(global::Creature __instance)
        {
            if (!Network.IsMultiplayerActive)
            {
                return true;
            }

            return !__instance.IsSynchronized();
        }
    }
}
