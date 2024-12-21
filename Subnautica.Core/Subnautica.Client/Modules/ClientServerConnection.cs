namespace Subnautica.Client.Modules
{
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;

    public static class ClientServerConnection
    {
        public static void OnInGameMenuOpened(InGameMenuOpenedEventArgs ev)
        {
            if (Network.IsMultiplayerActive)
            {
                if (IngameMenu.main.saveButton.gameObject.activeSelf)
                {
                    IngameMenu.main.saveButton.gameObject.SetActive(false);
                }

                if (IngameMenu.main.maxSecondsToBeRecentlySaved != 900000)
                {
                    IngameMenu.main.maxSecondsToBeRecentlySaved = 900000f;
                }
            }
            else
            {
                IngameMenu.main.saveButton.gameObject.SetActive(true);
                IngameMenu.main.maxSecondsToBeRecentlySaved = 120f;
            }
        }

        public static void OnInGameMenuClosed(InGameMenuClosedEventArgs ev)
        {
        }

        public static void OnSettingsRunInBackgroundChanging(SettingsRunInBackgroundChangingEventArgs ev)
        {
            if (Network.IsMultiplayerActive)
            {
                ev.IsAllowed = false;
            }
        }

        public static void OnSettingsPdaGamePauseChanging(SettingsPdaGamePauseChangingEventArgs ev)
        {
            if (Network.IsMultiplayerActive)
            {
                ev.IsAllowed = false;
            }
        }
    }
}