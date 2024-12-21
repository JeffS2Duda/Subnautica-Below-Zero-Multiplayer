namespace Subnautica.Events.Patches.Events.Building
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;
    using System.Collections;
    using UnityEngine;

    public static class ConstructingAmountChangedShared
    {
        public static bool IsTriggered { get; set; } = false;

        public static void TriggerEvent(Constructable __instance, bool isConstruct)
        {
            if (Network.IsMultiplayerActive && !IsTriggered)
            {
                UWE.CoroutineHost.StartCoroutine(TriggerEventCallback(__instance, isConstruct));
            }
        }

        private static IEnumerator TriggerEventCallback(Constructable __instance, bool isConstruct)
        {
            IsTriggered = true;

            yield return new WaitForSecondsRealtime(BroadcastInterval.ConstructingAmountChanged);

            IsTriggered = false;

            var constructedAmount = (float)Math.Round(__instance.constructedAmount, 4);
            if (constructedAmount > 0.99f && constructedAmount < 1f)
            {
                constructedAmount = 0.99f;
            }

            if (constructedAmount < 0.01f && constructedAmount > 0.00f)
            {
                constructedAmount = 0.01f;
            }

            if (__instance != null && constructedAmount > 0.0f && constructedAmount < 1f)
            {
                try
                {
                    ConstructionAmountChangedEventArgs args = new ConstructionAmountChangedEventArgs(__instance.techType, constructedAmount, isConstruct, Network.Identifier.GetIdentityId(__instance.gameObject));

                    Handlers.Building.OnConstructingAmountChanged(args);
                }
                catch (Exception e)
                {
                    Log.Error($"ConstructingAmountChangedShared.TriggerEventCallback: {e}\n{e.StackTrace}");
                }
            }
        }
    }

    [HarmonyPatch(typeof(Constructable), nameof(Constructable.Construct))]
    public static class ConstructingAmountChangedConstruct
    {
        private static void Postfix(Constructable __instance)
        {
            ConstructingAmountChangedShared.TriggerEvent(__instance, true);
        }
    }

    [HarmonyPatch(typeof(Constructable), nameof(Constructable.DeconstructAsync))]
    public static class ConstructingAmountChangedDeconstruct
    {
        private static void Postfix(Constructable __instance)
        {
            if (__instance._constructed || __instance.deconstructCoroutineRunning)
            {
                return;
            }

            ConstructingAmountChangedShared.TriggerEvent(__instance, false);
        }
    }
}
