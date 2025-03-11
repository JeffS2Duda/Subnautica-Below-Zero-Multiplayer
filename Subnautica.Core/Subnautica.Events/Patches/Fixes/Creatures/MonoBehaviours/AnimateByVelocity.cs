namespace Subnautica.Events.Patches.Fixes.Creatures.MonoBehaviours
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Creatures.MonoBehaviours.Shared;
    using UnityEngine;

    [HarmonyPatch]
    public class AnimateByVelocity
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::Creature), nameof(global::Creature.OnEnable))]
        public static void Creature_OnEnable(global::Creature __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                AnimateByVelocity.SetRootRigidbody(__instance.gameObject, true);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::Creature), nameof(global::Creature.OnDisable))]
        public static void Creature_OnDisable(global::Creature __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                AnimateByVelocity.SetRootRigidbody(__instance.gameObject, false);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::Creature), nameof(global::Creature.OnDestroy))]
        public static void Creature_OnDestroy(global::Creature __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                AnimateByVelocity.SetRootRigidbody(__instance.gameObject, false);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::VentGardenSmall), nameof(global::VentGardenSmall.OnEnable))]
        public static void VentGardenSmall_OnEnable(global::VentGardenSmall __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                AnimateByVelocity.SetRootRigidbody(__instance.gameObject, true);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::VentGardenSmall), nameof(global::VentGardenSmall.OnDisable))]
        public static void VentGardenSmall_OnDisable(global::VentGardenSmall __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                AnimateByVelocity.SetRootRigidbody(__instance.gameObject, false);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(MultiplayerWaterParkCreature), "OnAddToWP")]
        private static void MultiplayerWaterParkCreature_OnAddToWP(MultiplayerWaterParkCreature __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                AnimateByVelocity.SetRootRigidbody(__instance.gameObject, __instance.MultiplayerCreature.CreatureItem.IsMine(0));
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::AnimateByVelocity), nameof(global::AnimateByVelocity.Start))]
        public static bool Start(global::AnimateByVelocity __instance)
        {
            if (!Network.IsMultiplayerActive)
            {
                return true;
            }

            if (__instance.rootGameObject == null)
            {
                __instance.rootGameObject = __instance.gameObject;
            }

            __instance.animator.logWarnings = true;
            __instance.animator.SetFloat(AnimatorHashID.speed, 0.0f);
            __instance.animator.SetFloat(AnimatorHashID.pitch, 0.0f);
            __instance.animator.SetFloat(AnimatorHashID.tilt, 0.0f);
            __instance.previousPosition = __instance.rootGameObject.transform.position;
            return false;
        }

        public static void SetRootRigidbody(GameObject gameObject, bool isActive)
        {
            var animateByVelocity = gameObject.GetComponentInChildren<global::AnimateByVelocity>();
            if (animateByVelocity)
            {
                if (isActive)
                {
                    animateByVelocity.rootRigidbody = animateByVelocity.rootGameObject.GetComponent<Rigidbody>();
                }
                else
                {
                    animateByVelocity.rootRigidbody = null;
                }
            }
        }
    }
}
