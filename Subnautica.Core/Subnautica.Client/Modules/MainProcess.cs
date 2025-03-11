namespace Subnautica.Client.Modules
{
    using Subnautica.API.Features;
    using Subnautica.Client.Core;
    using Subnautica.Client.Multiplayer.Cinematics;
    using Subnautica.Events.EventArgs;
    using System;
    using System.Globalization;
    using System.Threading;

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
                CultureInfoEnabled();
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

        private static void CultureInfoEnabled()
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}