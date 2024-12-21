namespace Subnautica.Events.Patches.Events.Items
{
    using HarmonyLib;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;
    using System.Collections.Generic;

    [HarmonyPatch(typeof(global::CinematicModeTriggerBase), nameof(global::CinematicModeTriggerBase.OnHandClick))]
    public class Climbing
    {
        public static List<string> ClimbTexts = new List<string>()
        {
            "Climb",
            "ClimbUp",
            "ClimbDown",
            "ClimbLadder"
        };

        private static bool Prefix(global::CinematicModeTriggerBase __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                var uniqueId = Climbing.GetUniqueId(__instance);
                if (uniqueId.IsNull())
                {
                    return true;
                }

                try
                {
                    PlayerClimbingEventArgs args = new PlayerClimbingEventArgs(uniqueId, __instance.cinematicController?.director == null ? 0f : (float)__instance.cinematicController.director.duration);

                    Handlers.Player.OnClimbing(args);

                    return args.IsAllowed;
                }
                catch (Exception e)
                {
                    Log.Error($"Climbing.Prefix: {e}\n{e.StackTrace}");
                }
            }

            return true;
        }

        private static string GetUniqueId(global::CinematicModeTriggerBase __instance)
        {
            var constructor = __instance.GetComponentInParent<global::Constructor>();
            if (constructor)
            {
                return Network.Identifier.GetIdentityId(constructor.gameObject, false);
            }

            if (__instance.TryGetComponent<global::CinematicModeTrigger>(out var cinematic) && ClimbTexts.Contains(cinematic.handText))
            {
                return Network.Identifier.GetIdentityId(cinematic.gameObject, false);
            }

            return null;
        }
    }
}
