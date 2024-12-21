namespace Subnautica.Events.Patches.Events.Furnitures
{
    using HarmonyLib;
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;

    [HarmonyPatch(typeof(global::FiltrationMachine), nameof(global::FiltrationMachine.OnUse))]
    public static class FiltrationMachineOpening
    {
        private static bool Prefix(global::FiltrationMachine __instance, BaseFiltrationMachineGeometry model)
        {
            if (!Network.IsMultiplayerActive || __instance.constructed < 1f)
            {
                return true;
            }

            if (EventBlocker.IsEventBlocked(TechType.BaseFiltrationMachine))
            {
                return true;
            }

            try
            {
                FiltrationMachineOpeningEventArgs args = new FiltrationMachineOpeningEventArgs(Network.Identifier.GetIdentityId(model.gameObject, false));

                Handlers.Furnitures.OnFiltrationMachineOpening(args);

                return args.IsAllowed;
            }
            catch (Exception e)
            {
                Log.Error($"FiltrationMachineOpening.Prefix: {e}\n{e.StackTrace}");
                return true;
            }
        }
    }
}
