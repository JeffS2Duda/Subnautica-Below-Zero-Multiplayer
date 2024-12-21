namespace Subnautica.Events.Handlers
{
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class Inventory
    {
        public static event SubnauticaPluginEventHandler<InventoryItemRemovedEventArgs> ItemRemoved;

        public static void OnItemRemoved(InventoryItemRemovedEventArgs ev) => ItemRemoved.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<InventoryItemAddedEventArgs> ItemAdded;

        public static void OnItemAdded(InventoryItemAddedEventArgs ev) => ItemAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler QuickSlotBinded;

        public static void OnQuickSlotBinded() => QuickSlotBinded.CustomInvoke();

        public static event SubnauticaPluginEventHandler QuickSlotUnbinded;

        public static void OnQuickSlotUnbinded() => QuickSlotUnbinded.CustomInvoke();

        public static event SubnauticaPluginEventHandler EquipmentEquiped;

        public static void OnEquipmentEquiped() => EquipmentEquiped.CustomInvoke();

        public static event SubnauticaPluginEventHandler EquipmentUnequiped;

        public static void OnEquipmentUnequiped() => EquipmentUnequiped.CustomInvoke();

        public static event SubnauticaPluginEventHandler<QuickSlotActiveChangedEventArgs> QuickSlotActiveChanged;

        public static void OnQuickSlotActiveChanged(QuickSlotActiveChangedEventArgs ev) => QuickSlotActiveChanged.CustomInvoke(ev);
    }
}
