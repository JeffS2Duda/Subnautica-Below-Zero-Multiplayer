namespace Subnautica.Events.Patches.Fixes.Game;

using System;
using HarmonyLib;
using Subnautica.API.Features;
using Subnautica.Network.Structures;

[HarmonyPatch]
public static class StorageContainer
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(global::StorageContainer), "OnOpen")]
    private static bool OnOpen(global::StorageContainer __instance)
    {
        if (!Network.IsMultiplayerActive)
            return true;
        try
        {
            if (__instance.openSound && ZeroVector3.Distance(__instance.transform.position, ZeroPlayer.CurrentPlayer.Main.transform.position) <= 225f)
            {
                Utils.PlayFMODAsset(__instance.openSound, __instance.transform, 20f);
            }
            FMOD_CustomLoopingEmitter openLoopSound = __instance.openLoopSound;
            if (openLoopSound != null)
            {
                openLoopSound.Play();
            }
        }
        catch (Exception ex)
        {
            Log.Error(string.Format("StorageContainer.OnOpen Exception: {0}", ex));
        }
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(global::StorageContainer), "OnClose")]
    private static bool OnClose(global::StorageContainer __instance)
    {
        if (!Network.IsMultiplayerActive)
            return true;
        try
        {
            if (__instance.closeSound && ZeroVector3.Distance(__instance.transform.position, ZeroPlayer.CurrentPlayer.Main.transform.position) <= 225f)
            {
                Utils.PlayFMODAsset(__instance.closeSound, __instance.transform, 20f);
            }
            FMOD_CustomLoopingEmitter openLoopSound = __instance.openLoopSound;
            if (openLoopSound != null)
            {
                openLoopSound.Stop();
            }
        }
        catch (Exception ex)
        {
            Log.Error(string.Format("StorageContainer.OnClose Exception: {0}", ex));
        }
        return false;
    }
}
