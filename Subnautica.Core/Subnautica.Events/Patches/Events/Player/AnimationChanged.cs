namespace Subnautica.Events.Patches.Events.Player
{
    using HarmonyLib;
    using Subnautica.API.Enums;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;
    using System.Collections.Generic;

    [HarmonyPatch(typeof(ArmsController), nameof(ArmsController.Update))]
    public static class AnimationChanged
    {
        private static Dictionary<string, bool> AnimationStatusCache { get; set; } = new Dictionary<string, bool>();

        private static Dictionary<PlayerAnimationType, bool> ChangedAnimations { get; set; } = new Dictionary<PlayerAnimationType, bool>();

        private static void Postfix(ArmsController __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                AnimationChanged.ChangedAnimations.Clear();

                foreach (var animationName in PlayerAnimationTypeExtensions.Animations)
                {
                    var status = __instance.animator.GetBool(animationName);
                    if (AnimationChanged.IsAnimationChanged(animationName, status))
                    {
                        AnimationChanged.ChangedAnimations.Add(animationName.ToPlayerAnimationType(), status);
                    }
                }

                if (AnimationChanged.ChangedAnimations.Count > 0)
                {
                    try
                    {
                        PlayerAnimationChangedEventArgs args = new PlayerAnimationChangedEventArgs(AnimationChanged.ChangedAnimations);

                        Handlers.Player.OnAnimationChanged(args);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"AnimationChanged.Postfix: {e}\n{e.StackTrace}");
                    }
                }
            }
        }

        private static bool IsAnimationChanged(string animation, bool newStatus)
        {
            AnimationChanged.AnimationStatusCache.TryGetValue(animation, out bool oldStatus);
            AnimationChanged.AnimationStatusCache[animation] = newStatus;

            if (oldStatus != newStatus)
            {
                return true;
            }

            return false;
        }
    }
}
