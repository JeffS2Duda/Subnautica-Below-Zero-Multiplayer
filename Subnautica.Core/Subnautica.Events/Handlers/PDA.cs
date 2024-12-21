namespace Subnautica.Events.Handlers
{
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class PDA
    {
        public static event SubnauticaPluginEventHandler<EncyclopediaAddedEventArgs> EncyclopediaAdded;

        public static void OnEncyclopediaAdded(EncyclopediaAddedEventArgs ev) => EncyclopediaAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<TechnologyFragmentAddedEventArgs> TechnologyFragmentAdded;

        public static void OnTechnologyFragmentAdded(TechnologyFragmentAddedEventArgs ev) => TechnologyFragmentAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<TechnologyAddedEventArgs> TechnologyAdded;

        public static void OnTechnologyAdded(TechnologyAddedEventArgs ev) => TechnologyAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ScannerCompletedEventArgs> ScannerCompleted;

        public static void OnScannerCompleted(ScannerCompletedEventArgs ev) => ScannerCompleted.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler ItemPinAdded;

        public static void OnItemPinAdded() => ItemPinAdded.CustomInvoke();

        public static event SubnauticaPluginEventHandler ItemPinRemoved;

        public static void OnItemPinRemoved() => ItemPinRemoved.CustomInvoke();

        public static event SubnauticaPluginEventHandler ItemPinMoved;

        public static void OnItemPinMoved() => ItemPinMoved.CustomInvoke();

        public static event SubnauticaPluginEventHandler<PDALogAddedEventArgs> LogAdded;

        public static void OnLogAdded(PDALogAddedEventArgs ev) => LogAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<NotificationToggleEventArgs> NotificationToggle;

        public static void OnNotificationToggle(NotificationToggleEventArgs ev) => NotificationToggle.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<TechAnalyzeAddedEventArgs> TechAnalyzeAdded;

        public static void OnTechAnalyzeAdded(TechAnalyzeAddedEventArgs ev) => TechAnalyzeAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<JukeboxDiskAddedEventArgs> JukeboxDiskAdded;

        public static void OnJukeboxDiskAdded(JukeboxDiskAddedEventArgs ev) => JukeboxDiskAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PDAClosingEventArgs> Closing;

        public static void OnClosing(PDAClosingEventArgs ev) => Closing.CustomInvoke(ev);
    }
}
