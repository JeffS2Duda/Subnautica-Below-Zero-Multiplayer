namespace Subnautica.API.Features
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features.Helper;
    using System.Collections.Generic;
    using System.Text;

    public class Settings
    {

        public const string AuthorName = "BOT Benson";

        public const string RootFolder = "bz_multiplayer";

        public const string ApplicationFolder = "App";

        public const string GameFolder = "Game";

        public static ModConfigFormat ModConfig
        {
            get
            {
                if (modConfig == null)
                {
                    modConfig = new ModConfigFormat();
                    modConfig.Initialize();
                }

                return modConfig;
            }
        }

        public static bool IsBepinexInstalled { get; set; } = false;

        public static string LauncherVersion { get; set; }

        private static ModConfigFormat modConfig;

        public static string GetWatermarkText()
        {
            return string.Format("<size=18>Beta {0}/size>", Tools.GetLauncherVersion(true));
        }
    }
}
