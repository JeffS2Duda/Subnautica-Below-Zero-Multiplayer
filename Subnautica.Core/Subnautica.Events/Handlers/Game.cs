namespace Subnautica.Events.Handlers
{
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class Game
    {
        public static event SubnauticaPluginEventHandler Quitting;

        public static void OnQuitting() => Quitting.CustomInvoke();

        public static event SubnauticaPluginEventHandler<QuittingToMainMenuEventArgs> QuittingToMainMenu;

        public static void OnQuittingToMainMenu(QuittingToMainMenuEventArgs ev) => QuittingToMainMenu.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SceneLoadedEventArgs> SceneLoaded;

        public static void OnSceneLoaded(SceneLoadedEventArgs ev) => SceneLoaded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<MenuSaveCancelDeleteButtonClickingEventArgs> MenuSaveCancelDeleteButtonClicking;

        public static void OnMenuSaveCancelDeleteButtonClicking(MenuSaveCancelDeleteButtonClickingEventArgs ev) => MenuSaveCancelDeleteButtonClicking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<MenuSaveDeleteButtonClickingEventArgs> MenuSaveDeleteButtonClicking;

        public static void OnMenuSaveDeleteButtonClicking(MenuSaveDeleteButtonClickingEventArgs ev) => MenuSaveDeleteButtonClicking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<MenuSaveLoadButtonClickingEventArgs> MenuSaveLoadButtonClicking;

        public static void OnMenuSaveLoadButtonClicking(MenuSaveLoadButtonClickingEventArgs ev) => MenuSaveLoadButtonClicking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<MenuSaveUpdateLoadedButtonStateEventArgs> MenuSaveUpdateLoadedButtonState;

        public static void OnMenuSaveUpdateLoadedButtonState(MenuSaveUpdateLoadedButtonStateEventArgs ev) => MenuSaveUpdateLoadedButtonState.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<InGameMenuClosedEventArgs> InGameMenuClosed;

        public static void OnInGameMenuClosed(InGameMenuClosedEventArgs ev) => InGameMenuClosed.CustomInvoke(ev);
        
        public static event SubnauticaPluginEventHandler<InGameMenuClosingEventArgs> InGameMenuClosing;

        public static void OnInGameMenuClosing(InGameMenuClosingEventArgs ev) => InGameMenuClosing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<InGameMenuOpenedEventArgs> InGameMenuOpened;

        public static void OnInGameMenuOpened(InGameMenuOpenedEventArgs ev) => InGameMenuOpened.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<InGameMenuOpeningEventArgs> InGameMenuOpening;

        public static void OnInGameMenuOpening(InGameMenuOpeningEventArgs ev) => InGameMenuOpening.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SettingsRunInBackgroundChangingEventArgs> SettingsRunInBackgroundChanging;

        public static void OnSettingsRunInBackgroundChanging(SettingsRunInBackgroundChangingEventArgs ev) => SettingsRunInBackgroundChanging.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SettingsPdaGamePauseChangingEventArgs> SettingsPdaGamePauseChanging;

        public static void OnSettingsPdaGamePauseChanging(SettingsPdaGamePauseChangingEventArgs ev) => SettingsPdaGamePauseChanging.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<WorldLoadingEventArgs> WorldLoading;

        public static void OnWorldLoading(WorldLoadingEventArgs ev) => WorldLoading.CustomInvoke(ev);


        public static event SubnauticaPluginEventHandler<WorldLoadedEventArgs> WorldLoaded;

        public static void OnWorldLoaded(WorldLoadedEventArgs ev) => WorldLoaded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ScreenshotsRemovedEventArgs> ScreenshotsRemoved;

        public static void OnScreenshotsRemoved(ScreenshotsRemovedEventArgs ev) => ScreenshotsRemoved.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PowerSourceRemovingEventArgs> PowerSourceRemoving;

        public static void OnPowerSourceRemoving(PowerSourceRemovingEventArgs ev) => PowerSourceRemoving.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PowerSourceAddingEventArgs> PowerSourceAdding;

        public static void OnPowerSourceAdding(PowerSourceAddingEventArgs ev) => PowerSourceAdding.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<IntroCheckingEventArgs> IntroChecking;

        public static void OnIntroChecking(IntroCheckingEventArgs ev) => IntroChecking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<LifepodInterpolationEventArgs> LifepodInterpolation;

        public static void OnLifepodInterpolation(LifepodInterpolationEventArgs ev) => LifepodInterpolation.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<LifepodZoneCheckEventArgs> LifepodZoneCheck;

        public static void OnLifepodZoneCheck(LifepodZoneCheckEventArgs ev) => LifepodZoneCheck.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<LifepodZoneSelectingEventArgs> LifepodZoneSelecting;

        public static void OnLifepodZoneSelecting(LifepodZoneSelectingEventArgs ev) => LifepodZoneSelecting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SubNameInputSelectingEventArgs> SubNameInputSelecting;

        public static void OnSubNameInputSelecting(SubNameInputSelectingEventArgs ev) => SubNameInputSelecting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SubNameInputDeselectedEventArgs> SubNameInputDeselected;

        public static void OnSubNameInputDeselected(SubNameInputDeselectedEventArgs ev) => SubNameInputDeselected.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler EntityDistributionLoaded;

        public static void OnEntityDistributionLoaded() => EntityDistributionLoaded.CustomInvoke();
    }
}
