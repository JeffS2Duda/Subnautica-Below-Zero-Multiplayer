namespace Subnautica.API.Features
{
    using Subnautica.API.Extensions;
    using System;
    using System.IO;

    public class Paths
    {
        private static string _AppData { get; set; }

        private static string _CustomAppData { get; set; }

        public static string AppData
        {
            get
            {
                if (_CustomAppData.IsNotNull())
                {
                    return _CustomAppData;
                }

                if (_AppData == null)
                {
                    _AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                }

                return _AppData;
            }
        }

        public static char DS
        {
            get
            {
                return Path.DirectorySeparatorChar;
            }
        }

        public static void SetCustomAppDataPath(string customAppDataPath)
        {
            _CustomAppData = customAppDataPath;
        }

        public static string GetLauncherApiFileUrl()
        {
            return string.Format("{0}{1}", Settings.LauncherApiUrl, Settings.LauncherApiFile);
        }

        public static string GetLauncherLanguageUrl(string language)
        {
            return string.Format("{0}languages/{1}.json", Settings.GithubApiUrl, language);
        }

        public static string GetCreditsPageUrl()
        {
            return Settings.CreditsApiUrl;
        }

        public static string GetLauncherRemoteUrl(string version)
        {
            return string.Format("{0}Launchers/{1}.exe", Settings.LauncherApiUrl, version);
        }

        public static string GetLauncherRootPath()
        {
            return string.Format("{0}{1}{2}{3}", AppData, DS, Settings.RootFolder, DS);
        }

        public static string GetLauncherSubnauticaPath()
        {
            return string.Format("{0}{1}{2}", GetLauncherRootPath(), Settings.LauncherGameFolder, DS);
        }

        public static string GetLauncherApplicationPath(string subPath = null, bool addDS = true)
        {
            var applicationPath = GetLauncherSubnauticaPath();
            if (Settings.ApplicationFolder.Length <= 0)
            {
                return applicationPath;
            }

            if (subPath == null)
            {
                return string.Format("{0}{1}{2}", applicationPath, Settings.ApplicationFolder, DS);
            }

            if (addDS)
            {
                return string.Format("{0}{1}{2}{3}{4}", applicationPath, Settings.ApplicationFolder, DS, subPath, DS);
            }

            return string.Format("{0}{1}{2}{3}", applicationPath, Settings.ApplicationFolder, DS, subPath);
        }

        public static string GetLauncherGamePath(string subPath = null, bool addDS = true)
        {
            var applicationPath = GetLauncherSubnauticaPath();
            if (Settings.ApplicationFolder.Length <= 0)
            {
                return applicationPath;
            }

            if (subPath == null)
            {
                return string.Format("{0}{1}{2}", applicationPath, Settings.GameFolder, DS);
            }

            if (addDS)
            {
                return string.Format("{0}{1}{2}{3}{4}", applicationPath, Settings.GameFolder, DS, subPath, DS);
            }

            return string.Format("{0}{1}{2}{3}", applicationPath, Settings.GameFolder, DS, subPath);
        }

        public static string GetLauncherGameCorePath(string filename = null)
        {
            if (filename == null)
            {
                return GetLauncherGamePath("Core");
            }

            return String.Format("{0}{1}", GetLauncherGamePath("Core"), filename);
        }

        public static string GetLauncherGameCorePath(string foldername, string filename = null)
        {
            if (foldername == null)
            {
                return String.Format("{0}{1}{2}", GetLauncherGamePath("Core"), foldername, DS);
            }

            return String.Format("{0}{1}{2}{3}", GetLauncherGamePath("Core"), foldername, DS, filename);
        }

        public static string GetNetbirdPath(string filename = null)
        {
            return GetLauncherGameCorePath("NetBird", filename);
        }

        public static string GetGameDependenciesPath(string filename = null)
        {
            if (filename == null)
            {
                return GetLauncherGamePath("Dependencies");
            }

            return String.Format("{0}{1}", GetLauncherGamePath("Dependencies"), filename);
        }

        public static string GetGamePluginsPath(string filename = null)
        {
            if (filename == null)
            {
                return GetLauncherGamePath("Plugins");
            }

            return String.Format("{0}{1}", GetLauncherGamePath("Plugins"), filename);
        }

        public static string GetGameLogsPath()
        {
            return GetLauncherGamePath("Logs");
        }

        public static string GetMultiplayerSavePath(string foldername = null, string innerFolderName = null)
        {
            if (foldername == null)
            {
                return GetLauncherGamePath("Saves");
            }

            if (innerFolderName != null)
            {
                return string.Format("{0}{1}{2}{3}{4}", GetLauncherGamePath("Saves"), foldername, DS, innerFolderName, DS);
            }

            return string.Format("{0}{1}{2}", GetLauncherGamePath("Saves"), foldername, DS);
        }

        public static string GetMultiplayerServerSavePath(string serverId = null, string filename = null)
        {
            if (serverId == null)
            {
                return GetMultiplayerSavePath("Server");
            }

            if (filename == null)
            {
                return GetMultiplayerSavePath("Server", serverId);
            }

            return string.Format("{0}{1}", GetMultiplayerSavePath("Server", serverId), filename);
        }

        public static string GetMultiplayerClientSavePath(string serverId = null, string filename = null)
        {
            if (serverId == null)
            {
                return GetMultiplayerSavePath("Client");
            }

            if (filename == null)
            {
                return GetMultiplayerSavePath("Client", serverId);
            }

            return string.Format("{0}{1}", GetMultiplayerSavePath("Client", serverId), filename);
        }

        public static string GetMultiplayerClientSpawnPointPath(string serverId)
        {
            return GetMultiplayerClientSavePath(serverId, "SpawnPoint.bin");
        }

        public static string GetMultiplayerClientRemoteScreenshotsPath(string serverId, string filename = null)
        {
            var dataPath = GetMultiplayerClientSavePath(serverId, "RemoteScreenshots");
            if (filename == null)
            {
                return dataPath;
            }

            return string.Format("{0}{1}{2}", dataPath, DS, filename);
        }

        public static string GetMultiplayerClientRemoteScreenshotsThumbnailPath(string serverId, string filename)
        {
            var dataPath = GetMultiplayerClientSavePath(serverId, "RemoteScreenshots");
            if (filename == null)
            {
                return dataPath;
            }

            return GetMultiplayerClientRemoteScreenshotsPath(serverId, filename).Replace(".jpg", "_thumbnail.jpg");
        }

        public static string GetMultiplayerServerPlayerSavePath(string serverId, string playerUniqueId = null)
        {
            var dataPath = string.Format("{0}Players{1}", GetMultiplayerServerSavePath(serverId), DS);
            if (playerUniqueId == null)
            {
                return dataPath;
            }

            return string.Format("{0}{1}", dataPath, playerUniqueId);
        }

        public static string GetGameServersPath()
        {
            return GetLauncherGamePath("servers.json", false);
        }

        public static string GetLauncherLogPath()
        {
            return string.Format("{0}", GetLauncherApplicationPath("Logs"));
        }

        public static string GetLauncherLanguagePath()
        {
            return string.Format("{0}", GetLauncherApplicationPath("Languages"));
        }

        public static string GetLauncherLanguageFile(string language)
        {
            return string.Format("{0}{1}.json", GetLauncherApplicationPath("Languages"), language);
        }

        public static string GetSliderImagePath(string image = "")
        {
            return string.Format("{0}{1}", GetLauncherApplicationPath(Settings.ApplicationImageFolder), image);
        }

        public static string GetLauncherApiFilePath()
        {
            return string.Format("{0}{1}", GetLauncherApplicationPath(), Settings.LauncherApiFile);
        }

        public static string GetLauncherCreditsApiFilePath()
        {
            return string.Format("{0}{1}", GetLauncherApplicationPath(), Settings.LauncherCreditsApiFile);
        }

        public static string GetLauncherTempPath()
        {
            return string.Format("{0}", GetLauncherApplicationPath(Settings.ApplicationTempFolder));
        }

        public static string GetLauncherTempFile()
        {
            return string.Format("{0}Temp.exe", GetLauncherApplicationPath(Settings.ApplicationTempFolder));
        }

        public static string GetLauncherNewVersionFile()
        {
            return string.Format("{0}NewVersion.exe", GetLauncherApplicationPath(Settings.ApplicationTempFolder));
        }

        public static string GetNowLauncherFile()
        {
            return System.Reflection.Assembly.GetEntryAssembly().Location;
        }
    }
}