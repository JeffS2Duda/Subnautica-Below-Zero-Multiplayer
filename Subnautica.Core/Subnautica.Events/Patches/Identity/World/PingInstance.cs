namespace Subnautica.Events.Patches.Identity.World
{
    using HarmonyLib;

    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;

    using UnityEngine;

    [HarmonyPatch(typeof(global::PingInstance), nameof(global::PingInstance.Start))]
    public static class PingInstance
    {
        private static void Prefix(global::PingInstance __instance)
        {
            __instance.gameObject.WaitForInitialize(CheckAction, SuccessAction);
        }

        private static bool CheckAction(GameObject gameObject, int currentTick)
        {
            return currentTick >= 5;
        }

        private static void SuccessAction(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<global::PingInstance>(out var pingInstance))
            {
                pingInstance.RemoveNotificationInternal();
                pingInstance.ResetFakePosition();

                PingManager.Unregister(pingInstance);

                if (pingInstance.TryGetComponent<SignalPing>(out var signalPing))
                {
                    pingInstance._id = Network.Identifier.GetWorldEntityId(signalPing.pos, "SignalPing");

                    Network.Identifier.SetIdentityId(pingInstance.gameObject, pingInstance._id);
                }
                else
                {
                    pingInstance._id = pingInstance.gameObject.GetIdentityId();
                }

                PingManager.Register(pingInstance);

                pingInstance.initialized = true;

                using (EventBlocker.Create(ProcessType.NotificationAdded))
                {
                    pingInstance.AddNotificationInternal();
                }
            }
        }
    }
}
