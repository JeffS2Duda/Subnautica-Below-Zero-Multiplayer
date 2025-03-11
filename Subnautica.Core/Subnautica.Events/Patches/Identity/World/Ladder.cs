namespace Subnautica.Events.Patches.Identity.World
{
    using HarmonyLib;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Events.Patches.Events.Items;
    using System.Collections;
    using UnityEngine;

    [HarmonyPatch(typeof(global::CinematicModeTriggerBase), nameof(global::CinematicModeTriggerBase.OnEnable))]
    public class Ladder
    {
        private static void Prefix(global::CinematicModeTriggerBase __instance)
        {
            if (Network.IsMultiplayerActive && __instance.TryGetComponent<global::CinematicModeTrigger>(out var cinematic) && Climbing.ClimbTexts.Contains(cinematic.handText))
            {
                __instance.StartCoroutine(Ladder.AutoAssignUniqueId(__instance, Ladder.GetLadderUniqueId(cinematic)));
            }
        }

        private static IEnumerator AutoAssignUniqueId(global::CinematicModeTriggerBase __instance, string uniqueId)
        {
            yield return new WaitForSecondsRealtime(0.25f);
            if (__instance)
                ((Component)__instance).gameObject.SetIdentityId(uniqueId);
        }

        public static string GetLadderUniqueId(global::CinematicModeTrigger cinematic)
        {
            GameObject ladder = null;

            foreach (var item in cinematic.GetComponentsInParent<Component>())
            {
                if (item.name == "Ladder" || item.name == "ladder1_cin")
                {
                    ladder = item.gameObject;
                    break;
                }
            }

            if (ladder == null)
            {
                return Network.Identifier.GetWorldEntityId(cinematic.transform.position, cinematic.handText);
            }

            return Network.Identifier.GetWorldEntityId(ladder.transform.position, cinematic.name);
        }
    }
}
