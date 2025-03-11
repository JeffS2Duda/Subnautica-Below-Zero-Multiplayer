namespace Subnautica.Events.Patches.Fixes.Interact;

using System;
using HarmonyLib;
using Subnautica.API.Extensions;
using Subnautica.API.Features;
using UnityEngine;

[HarmonyPatch(typeof(global::LargeRoomWaterParkPlanter), "IsOnHandOverActive")]
public class LargeRoomWaterParkPlanter
{
    private static bool Prefix(global::LargeRoomWaterParkPlanter __instance)
    {
        if (!Network.IsMultiplayerActive)
        {
            return true;
        }
        LargeRoomWaterPark componentInParent = __instance.GetComponentInParent<LargeRoomWaterPark>();
        bool flag3 = componentInParent == null;
        if (flag3)
        {
            return false;
        }
        else
        {
            BaseDeconstructable baseDeconstructable = componentInParent.GetBaseDeconstructable();
            string text;
            if (baseDeconstructable == null)
            {
                text = null;
            }
            else
            {
                GameObject gameObject = baseDeconstructable.gameObject;
                text = ((gameObject != null) ? gameObject.GetIdentityId(false) : null);
            }
            string text2 = text;
            bool flag4 = text2.IsNull();
            if (flag4)
            {
                return false;
            }
            else
            {
                bool flag5 = Network.HandTarget.IsBlocked(text2);
                if (flag5)
                {
                    Interact.ShowUseDenyMessage();
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
