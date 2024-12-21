namespace Subnautica.Events.Patches.Fixes.Game
{
    using HarmonyLib;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using System;

    [HarmonyPatch]
    public static class IntroText
    {
        private static string OriginalText { get; set; }

        private static void TogglePresentText()
        {
            if (OriginalText.IsNotNull())
            {
                try
                {
                    if (Network.IsMultiplayerActive)
                    {
                        global::Language.main.strings["IntroUWEPresents"] = string.Format("{0}\n\n\n\n\n\n\n\n\n<size=\"18\">{1}</size>", OriginalText, ZeroLanguage.Get("GAME_INTRO_MULTIPLAYER_BY_BOTBENSON"));
                    }
                    else
                    {
                        global::Language.main.strings["IntroUWEPresents"] = OriginalText;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"uGUI_MainMenu.StartNewGame: {ex}");
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(global::MainMenuMusic), nameof(global::MainMenuMusic.Start))]
        private static void Language_SetCurrentLanguage(global::MainMenuMusic __instance)
        {
            if (OriginalText.IsNull())
            {
                OriginalText = global::Language.main.Get("IntroUWEPresents");
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(global::uGUI_MainMenu), nameof(global::uGUI_MainMenu.StartNewGame))]
        private static void uGUI_MainMenu_StartNewGame(global::uGUI_MainMenu __instance)
        {
            TogglePresentText();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(global::uGUI_MainMenu), nameof(global::uGUI_MainMenu.LoadGameAsync))]
        private static void uGUI_MainMenu_LoadGameAsync(global::uGUI_MainMenu __instance)
        {
            TogglePresentText();
        }
    }
}
