namespace Subnautica.Events.Patches.Events.Furnitures;

using System;
using System.Collections.Generic;
using HarmonyLib;
using Subnautica.API.Extensions;
using Subnautica.API.Features;
using Subnautica.Events.EventArgs;
using Subnautica.Events.Handlers;

[HarmonyPatch(typeof(Planter), "ResetStorage")]
public static class PlanterStorageReseting
{
    private static bool Prefix(Planter __instance)
    {
        if (!Network.IsMultiplayerActive)
            return true;
        KeyValuePair<string, KeyValuePair<TechType, bool>> detail = __instance.GetDetail();
        if (detail.Key == null)
            return true;
        try
        {
            PlanterStorageResetingEventArgs ev = new PlanterStorageResetingEventArgs(detail.Key, detail.Value.Key, detail.Value.Value);
            Subnautica.Events.Handlers.Furnitures.OnPlanterStorageReseting(ev);
            return ev.IsAllowed;
        }
        catch (Exception ex)
        {
            Log.Error(string.Format("PlanterStorageReseting.Prefix: {0}\n{1}", (object)ex, (object)ex.StackTrace));
            return true;
        }
    }
}
