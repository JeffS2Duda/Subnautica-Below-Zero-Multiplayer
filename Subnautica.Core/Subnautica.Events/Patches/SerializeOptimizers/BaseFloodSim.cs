namespace Subnautica.Events.Patches.Identity.Building
{
    using HarmonyLib;

    using Subnautica.API.Features;

    [HarmonyPatch(typeof(ProtobufSerializerPrecompiled), nameof(ProtobufSerializerPrecompiled.Serialize1725266300))]
    public class BaseFloodSim
    {
        private static bool Prefix()
        {
            return !Network.IsMultiplayerActive;
        }
    }
}
