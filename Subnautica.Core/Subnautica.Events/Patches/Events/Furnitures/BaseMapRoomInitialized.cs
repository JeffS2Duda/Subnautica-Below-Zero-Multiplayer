namespace Subnautica.Events.Patches.Events.Furnitures
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;

    [HarmonyPatch(typeof(global::uGUI_MapRoomScanner), nameof(global::uGUI_MapRoomScanner.Start))]
    public static class BaseMapRoomInitialized
    {
        private static void Postfix(global::uGUI_MapRoomScanner __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                try
                {
                    BaseMapRoomInitializedEventArgs args = new BaseMapRoomInitializedEventArgs(__instance);

                    Handlers.Furnitures.OnBaseMapRoomInitialized(args);
                }
                catch (Exception e)
                {
                    Log.Error($"BaseMapRoomInitialized.Postfix: {e}\n{e.StackTrace}");
                }
            }
        }
    }
}
