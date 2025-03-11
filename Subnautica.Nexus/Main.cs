using Subnautica.API.Features;
using Subnautica.Nexus.Handlers;
using System.IO;

namespace Subnautica.Nexus;

public sealed class Main : SubnauticaPlugin
{
    public static string Dir
    {
        get
        {
            return Path.GetDirectoryName(typeof(Main).Assembly.Location);
        }
    }
    public override string Name { get; } = "Subnautica Nexus Plugin";
    public override void OnEnabled()
    {
        base.OnEnabled();
        Events.Handlers.Inventory.InventoryLoseItems += InventoryHandler.Inventory_InventoryLoseItems;
    }

    public override void OnDisabled()
    {
        base.OnDisabled();
        Events.Handlers.Inventory.InventoryLoseItems -= InventoryHandler.Inventory_InventoryLoseItems;
    }
}
