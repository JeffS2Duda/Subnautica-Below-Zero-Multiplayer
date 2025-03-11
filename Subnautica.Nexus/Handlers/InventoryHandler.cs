namespace Subnautica.Nexus.Handlers;

public class InventoryHandler
{
    public static void Inventory_InventoryLoseItems(Events.EventArgs.InventoryLoseItemsEventArgs ev)
    {
        ev.ShouldLose = false;
        ev.Items = [];
    }
}
