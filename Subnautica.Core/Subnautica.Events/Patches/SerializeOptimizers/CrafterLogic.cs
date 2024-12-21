namespace Subnautica.Events.Patches.Identity.Building
{
    using HarmonyLib;

    using Subnautica.API.Features;

    [HarmonyPatch(typeof(ProtobufSerializerPrecompiled), nameof(ProtobufSerializerPrecompiled.Serialize2100222829))]
    public class CrafterLogic
    {
        private static bool Prefix()
        {
            return !Network.IsMultiplayerActive;
        }
    }
}
