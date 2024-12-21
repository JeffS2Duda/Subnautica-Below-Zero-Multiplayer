namespace Subnautica.Client.Modules
{
    using System;

    using Subnautica.API.Features;
    using Subnautica.Client.Core;
    using Subnautica.Client.Multiplayer.Cinematics;
    using Subnautica.Events.EventArgs;

    public class MainProcess
    {
        public static void OnPluginEnabled()
        {
            ZeroLanguage.LoadLanguage(Tools.GetLanguage());
        }

        public static void OnQuittingToMainMenu(QuittingToMainMenuEventArgs ev)
        {
            ClearAllCache();
        }

        public static void OnSceneLoaded(SceneLoadedEventArgs ev)
        {
            if (ev.Scene.name == "XMenu")
            {
                ClearAllCache();
            }
        }

        public static void ClearAllCache()
        {
            try
            {
                QualitySetting.DisableFastMode();
                Network.Dispose();
                NetworkServer.AbortServer();
                NetworkClient.Disconnect();

                PlayerCinematicQueue.Dispose();
                Multiplayer.Furnitures.Bed.Dispose();
            }
            catch (Exception e)
            {
                Log.Error($"ClearAllCache Exception: {e}");
            }
        }
    }
}