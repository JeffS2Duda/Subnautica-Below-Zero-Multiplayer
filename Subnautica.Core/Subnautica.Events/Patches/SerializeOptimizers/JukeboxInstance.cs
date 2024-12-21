namespace Subnautica.Events.Patches.Identity.Building
{
    using HarmonyLib;

    using Subnautica.API.Features;

    [HarmonyPatch(typeof(ProtobufSerializerPrecompiled), nameof(ProtobufSerializerPrecompiled.Serialize1662769415))]
    public class JukeboxInstance
    {
        private static bool Prefix()
        {
            return !Network.IsMultiplayerActive;
        }
    }
}
