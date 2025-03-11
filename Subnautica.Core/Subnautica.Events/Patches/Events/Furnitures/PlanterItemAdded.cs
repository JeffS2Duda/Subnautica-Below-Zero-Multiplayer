namespace Subnautica.Events.Patches.Events.Furnitures
{
    using HarmonyLib;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;

    using System;
    using System.Collections.Generic;

    [HarmonyPatch(typeof(global::Planter), nameof(global::Planter.AddItem), new Type[] { typeof(Plantable), typeof(int) })]
    public static class PlanterItemAdded
    {
        private static void Postfix(global::Planter __instance, Plantable plantable, int slotID)
        {
            if (!Network.IsMultiplayerActive)
            {
                return;
            }

            if (__instance.constructable == null || EventBlocker.IsEventBlocked(__instance.constructable.techType))
            {
                return;
            }

            try
            {
                KeyValuePair<string, KeyValuePair<TechType, bool>> detail = __instance.GetDetail();
                if (detail.Key.IsNull() || EventBlocker.IsEventBlocked(detail.Value.Key))
                    return;
                Handlers.Furnitures.OnPlanterItemAdded(new PlanterItemAddedEventArgs(detail.Key, plantable.gameObject.GetIdentityId(), plantable, slotID, detail.Value.Value));
            }
            catch (Exception e)
            {
                Log.Error($"PlanterItemAdded.Prefix: {e}\n{e.StackTrace}");
            }
        }
    }
}
