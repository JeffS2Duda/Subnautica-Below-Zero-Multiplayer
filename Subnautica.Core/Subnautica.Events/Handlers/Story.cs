namespace Subnautica.Events.Handlers
{
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class Story
    {
        public static event SubnauticaPluginEventHandler<BridgeFluidClickingEventArgs> BridgeFluidClicking;

        public static void OnBridgeFluidClicking(BridgeFluidClickingEventArgs ev) => BridgeFluidClicking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BridgeTerminalClickingEventArgs> BridgeTerminalClicking;

        public static void OnBridgeTerminalClicking(BridgeTerminalClickingEventArgs ev) => BridgeTerminalClicking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BridgeInitializedEventArgs> BridgeInitialized;

        public static void OnBridgeInitialized(BridgeInitializedEventArgs ev) => BridgeInitialized.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<RadioTowerTOMUsingEventArgs> RadioTowerTOMUsing;

        public static void OnRadioTowerTOMUsing(RadioTowerTOMUsingEventArgs ev) => RadioTowerTOMUsing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<StorySignalSpawningEventArgs> StorySignalSpawning;

        public static void OnStorySignalSpawning(StorySignalSpawningEventArgs ev) => StorySignalSpawning.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<StoryGoalTriggeringEventArgs> StoryGoalTriggering;

        public static void OnStoryGoalTriggering(StoryGoalTriggeringEventArgs ev) => StoryGoalTriggering.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CinematicTriggeringEventArgs> CinematicTriggering;

        public static void OnCinematicTriggering(CinematicTriggeringEventArgs ev) => CinematicTriggering.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<StoryCallingEventArgs> StoryCalling;

        public static void OnStoryCalling(StoryCallingEventArgs ev) => StoryCalling.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<StoryHandClickingEventArgs> StoryHandClicking;

        public static void OnStoryHandClicking(StoryHandClickingEventArgs ev) => StoryHandClicking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<StoryCinematicStartedEventArgs> StoryCinematicStarted;

        public static void OnStoryCinematicStarted(StoryCinematicStartedEventArgs ev) => StoryCinematicStarted.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<StoryCinematicCompletedEventArgs> StoryCinematicCompleted;

        public static void OnStoryCinematicCompleted(StoryCinematicCompletedEventArgs ev) => StoryCinematicCompleted.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler MobileExtractorMachineInitialized;

        public static void OnMobileExtractorMachineInitialized() => MobileExtractorMachineInitialized.CustomInvoke();

        public static event SubnauticaPluginEventHandler<MobileExtractorMachineSampleAddingEventArgs> MobileExtractorMachineSampleAdding;

        public static void OnMobileExtractorMachineSampleAdding(MobileExtractorMachineSampleAddingEventArgs ev) => MobileExtractorMachineSampleAdding.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<MobileExtractorConsoleUsingEventArgs> MobileExtractorConsoleUsing;

        public static void OnMobileExtractorConsoleUsing(MobileExtractorConsoleUsingEventArgs ev) => MobileExtractorConsoleUsing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ShieldBaseEnterTriggeringEventArgs> ShieldBaseEnterTriggering;

        public static void OnShieldBaseEnterTriggering(ShieldBaseEnterTriggeringEventArgs ev) => ShieldBaseEnterTriggering.CustomInvoke(ev);
    }
}
