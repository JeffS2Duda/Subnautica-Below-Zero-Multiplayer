namespace Subnautica.Events.Patches.Events.Furnitures
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using System;
    using System.IO;

    [HarmonyPatch(typeof(ScreenshotManager.LoadingRequest), MethodType.Constructor, new Type[] { typeof(string), typeof(string) })]
    public static class ScreenshotsFilename
    {
        private static void Postfix(ScreenshotManager.LoadingRequest __instance, string fileName, string url)
        {
            if (Network.IsMultiplayerActive)
            {
                string filePath = Paths.GetMultiplayerClientRemoteScreenshotsPath(ZeroPlayer.CurrentPlayer.CurrentServerId, fileName);
                if (File.Exists(filePath))
                {
                    __instance.path = filePath;
                }
            }
        }
    }
}
