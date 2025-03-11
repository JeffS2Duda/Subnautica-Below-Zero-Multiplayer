namespace Subnautica.Events.Patches.Fixes.Interact
{
    using HarmonyLib;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Events.Patches.Events.Storage;
    using System.Collections.Generic;
    using UnityEngine;

    [HarmonyPatch(typeof(global::StorageContainer), nameof(global::StorageContainer.OnHandHover))]
    public class StorageContainer
    {
        private static bool Prefix(global::StorageContainer __instance)
        {
            if (!Network.IsMultiplayerActive)
                return true;
            if (!__instance.enabled || __instance.disableUseability)
                return false;
            KeyValuePair<string, TechType> storageDetail = Opening.GetStorageDetail(__instance);
            if (storageDetail.Key.IsNull())
                return false;
            if (!Interact.IsBlocked(storageDetail.Key))
                return true;
            Interact.ShowUseDenyMessage();
            return false;
        }
    }
}
