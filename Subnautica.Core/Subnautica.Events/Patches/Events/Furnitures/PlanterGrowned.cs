namespace Subnautica.Events.Patches.Events.Furnitures
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;

    [HarmonyPatch(typeof(global::FruitPlant), nameof(global::FruitPlant.OnGrown))]
    public static class PlanterGrowned
    {
        private static void Postfix(global::FruitPlant __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                try
                {
                    PlanterGrownedEventArgs args = new PlanterGrownedEventArgs(__instance);

                    Handlers.Furnitures.OnPlanterGrowned(args);
                }
                catch (Exception e)
                {
                    Log.Error($"PlanterGrowned.Postfix: {e}\n{e.StackTrace}");
                }
            }
        }
    }
}
