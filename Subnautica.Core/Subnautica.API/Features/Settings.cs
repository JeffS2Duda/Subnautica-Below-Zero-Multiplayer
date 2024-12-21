namespace Subnautica.API.Features
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features.Helper;
    using System.Collections.Generic;
    using System.Text;

    public class Settings
    {
        public const string LauncherApiUrl = "https://repo.subnauticamultiplayer.com/beta/";

        public const string GithubApiUrl = "https://raw.githubusercontent.com/ismail0234/Subnautica-Below-Zero-Multiplayer/";

        public const string CreditsApiUrl = "https://raw.githubusercontent.com/ismail0234/Subnautica-Below-Zero-Multiplayer/main/credits.json";

        public const string AuthorName = "BOT Benson";

        public const string DiscordClientId = "806248184405688380";

        public const string LauncherApiFile = "Api.json";

        public const string LauncherCreditsApiFile = "credits.json";

        public const string RootFolder = ".botbenson";

        public const string ApplicationFolder = "App";

        public const string GameFolder = "Game";

        public const string ApplicationTempFolder = "Tmp";

        public const string ApplicationImageFolder = "Images";

        public const string LauncherGameFolder = "Subnautica Below Zero";

        public static ApiDataFormat Api { get; set; }

        public static ApiCreditsDataFormat CreditsApi { get; set; }

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

        public static bool IsAppLog { get; set; } = false;

        public static bool IsBepinexInstalled { get; set; } = false;

        public static string LauncherVersion { get; set; }

        private static ModConfigFormat modConfig;

        public static string GetWatermarkText()
        {
            return string.Format("<size=18>Beta {0} (by BOT Benson)</size>", Tools.GetLauncherVersion(true));
        }

        public static string GetCreditsText()
        {
            var api = Tools.GetCreditsApiData();
            if (api != null)
            {
                var testers = new List<string>();
                var credits = new StringBuilder();

                credits.AppendLine("<style=role>Multiplayer Mod Creator / Programmer</style>");
                foreach (var item in api.ProjectOwner.Members)
                {
                    credits.AppendLine(item.Name);
                }

                credits.AppendLine("\n<style=role>Multiplayer Mod Patreon Supporters</style>");
                foreach (var item in api.PatreonSupporters.Members)
                {
                    credits.AppendLine(item.Name);
                }

                credits.AppendLine("\n<style=role>Multiplayer Mod Translators</style>");
                foreach (var item in api.Translators.Members)
                {
                    credits.AppendLine(item.Name);
                }

                credits.AppendLine("\n<style=role>Multiplayer Mod Alpha Testers</style>");

                if (api.AlphaTesters.Members.Count > 500)
                {
                    credits.AppendLine("2000+ Alpha Testers <3");
                }
                else
                {
                    foreach (var items in api.AlphaTesters.Members.Split(3))
                    {
                        testers.Clear();

                        foreach (var item in items)
                        {
                            testers.Add(item.Name);
                        }

                        credits.AppendLine(string.Join(" / ", testers));
                    }
                }

                return credits.ToString();
            }

            return null;
        }
    }
}
