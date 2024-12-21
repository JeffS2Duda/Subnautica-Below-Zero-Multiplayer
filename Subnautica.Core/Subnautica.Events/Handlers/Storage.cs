namespace Subnautica.Events.Handlers
{
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class Storage
    {
        public static event SubnauticaPluginEventHandler<StorageOpeningEventArgs> Opening;

        public static void OnOpening(StorageOpeningEventArgs ev) => Opening.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<StorageItemAddedEventArgs> ItemAdded;

        public static void OnItemAdded(StorageItemAddedEventArgs ev) => ItemAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<StorageItemRemovedEventArgs> ItemRemoved;

        public static void OnItemRemoved(StorageItemRemovedEventArgs ev) => ItemRemoved.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<NuclearReactorItemAddedEventArgs> NuclearReactorItemAdded;

        public static void OnNuclearReactorItemAdded(NuclearReactorItemAddedEventArgs ev) => NuclearReactorItemAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<NuclearReactorItemRemovedEventArgs> NuclearReactorItemRemoved;

        public static void OnNuclearReactorItemRemoved(NuclearReactorItemRemovedEventArgs ev) => NuclearReactorItemRemoved.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ChargerItemAddedEventArgs> ChargerItemAdded;

        public static void OnChargerItemAdded(ChargerItemAddedEventArgs ev) => ChargerItemAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ChargerItemRemovedEventArgs> ChargerItemRemoved;

        public static void OnChargerItemRemoved(ChargerItemRemovedEventArgs ev) => ChargerItemRemoved.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<StorageItemRemovingEventArgs> ItemRemoving;

        public static void OnItemRemoving(StorageItemRemovingEventArgs ev) => ItemRemoving.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<StorageItemAddingEventArgs> ItemAdding;

        public static void OnItemAdding(StorageItemAddingEventArgs ev) => ItemAdding.CustomInvoke(ev);
    }
}
