namespace Subnautica.Events.EventArgs;

using System;
using System.Collections.Generic;

public class InventoryLoseItemsEventArgs : EventArgs
{
    public InventoryLoseItemsEventArgs(List<InventoryItem> items, bool shouldLose = true)
    {
        this.Items = items;
        this.ShouldLose = shouldLose;
    }

    public List<InventoryItem> Items { get; set; }
    public bool ShouldLose { get; set; } = true;
}