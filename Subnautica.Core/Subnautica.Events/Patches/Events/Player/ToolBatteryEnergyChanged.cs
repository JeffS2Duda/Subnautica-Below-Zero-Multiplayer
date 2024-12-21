namespace Subnautica.Events.Patches.Events.Player
{
    using HarmonyLib;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [HarmonyPatch]
    public static class ToolBatteryEnergyChanged
    {
        private static StopwatchItem Timing = new StopwatchItem(1000f);

        private static Dictionary<string, BatteryEnergyItem> Items = new Dictionary<string, BatteryEnergyItem>();

        [HarmonyPostfix]
        [HarmonyPatch(typeof(global::QuickSlots), nameof(global::QuickSlots.Update))]
        private static void QuickSlots_Update(global::QuickSlots __instance)
        {
            if (Network.IsMultiplayerActive && Timing.IsFinished())
            {
                Timing.Restart();

                if (__instance.heldItem?.item != null && __instance.heldItem.item.gameObject.TryGetComponent<global::EnergyMixin>(out var energyMixin))
                {
                    var uniqueId = __instance.heldItem.item.gameObject.GetIdentityId();
                    if (uniqueId.IsNotNull() && (!Items.TryGetValue(uniqueId, out var batteryItem) || IsBatteryValueChanged(batteryItem, energyMixin)))
                    {
                        if (batteryItem == null)
                        {
                            batteryItem = new BatteryEnergyItem();
                        }

                        batteryItem.SetBattery(energyMixin.charge, energyMixin.capacity);

                        Items[uniqueId] = batteryItem;

                        try
                        {
                            ToolBatteryEnergyChangedEventArgs args = new ToolBatteryEnergyChangedEventArgs(uniqueId, __instance.heldItem.item);

                            Handlers.Player.OnToolBatteryEnergyChanged(args);
                        }
                        catch (Exception e)
                        {
                            Log.Error($"ToolBatteryEnergyChanged.QuickSlots_Update: {e}\n{e.StackTrace}");
                        }
                    }
                }
            }
        }

        private static bool IsBatteryValueChanged(BatteryEnergyItem batteryItem, global::EnergyMixin energyMixin)
        {
            if (Mathf.Abs(batteryItem.Charge - energyMixin.charge) >= 1f)
            {
                return true;
            }

            if (Mathf.Abs(batteryItem.Capacity - energyMixin.capacity) >= 1f)
            {
                return true;
            }

            return false;
        }
    }
    public class BatteryEnergyItem
    {
        public float Charge { get; set; } = -1f;

        public float Capacity { get; set; } = -1f;

        public void SetBattery(float charge, float capacity)
        {
            this.Charge = charge;
            this.Capacity = capacity;
        }
    }
}
