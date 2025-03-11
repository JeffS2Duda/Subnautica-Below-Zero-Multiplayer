namespace Subnautica.Events.Patches.Fixes.Vehicle;

using HarmonyLib;
using Subnautica.API.Extensions;
using Subnautica.API.Features;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UWE;

[HarmonyPatch(typeof(global::Hoverbike), "OnProtoDeserializeObjectTree")]
public class Hoverbike
{
    private static bool Prefix(global::Hoverbike __instance)
    {
        if (!Network.IsMultiplayerActive)
            return true;
        try
        {
            if (__instance.serializedModuleSlots != null)
            {
                KeyValuePair<string, string> keyValuePair = __instance.serializedModuleSlots.FirstOrDefault<KeyValuePair<string, string>>();
                if (keyValuePair.Value.IsNotNull())
                {
                    StorageHelper.TransferEquipment(((Component)__instance.modulesRoot).gameObject, __instance.serializedModuleSlots, __instance.modules);
                    if (__instance.modules.GetItemInSlot(keyValuePair.Key) == null)
                        CoroutineHost.StartCoroutine(Hoverbike.AddHoverbikeModule(__instance, keyValuePair.Key, keyValuePair.Value));
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(string.Format("Hoverbike.OnProtoDeserializeObjectTree Exception: {0}", (object)ex));
        }
        finally
        {
            __instance.serializedModuleSlots = (Dictionary<string, string>)null;
            __instance.UnlockDefaultModuleSlots();
        }
        return false;
    }

    private static IEnumerator AddHoverbikeModule(
      global::Hoverbike __instance,
      string slotId,
      string itemId)
    {
        TaskResult<GameObject> task = new TaskResult<GameObject>();
        yield return (object)CraftData.InstantiateFromPrefabAsync(TechType.HoverbikeJumpModule, (IOut<GameObject>)task);
        GameObject gameObject = task.Get();
        if (gameObject)
        {
            gameObject.SetIdentityId(itemId);
            __instance.modules.AddItem(slotId, new InventoryItem(Radical.EnsureComponent<Pickupable>(gameObject)), true);
        }
    }
}
