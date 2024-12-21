namespace Subnautica.Events.Handlers
{
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class Items
    {
        public static event SubnauticaPluginEventHandler<KnifeUsingEventArgs> KnifeUsing;

        public static void OnKnifeUsing(KnifeUsingEventArgs ev) => KnifeUsing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ScannerUsingEventArgs> ScannerUsing;

        public static void OnScannerUsing(ScannerUsingEventArgs ev) => ScannerUsing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ConstructorDeployingEventArgs> ConstructorDeploying;

        public static void OnConstructorDeploying(ConstructorDeployingEventArgs ev) => ConstructorDeploying.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ConstructorEngageToggleEventArgs> ConstructorEngageToggle;

        public static void OnConstructorEngageToggle(ConstructorEngageToggleEventArgs ev) => ConstructorEngageToggle.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ConstructorCraftingEventArgs> ConstructorCrafting;

        public static void OnConstructorCrafting(ConstructorCraftingEventArgs ev) => ConstructorCrafting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<HoverbikeDeployingEventArgs> HoverbikeDeploying;

        public static void OnHoverbikeDeploying(HoverbikeDeployingEventArgs ev) => HoverbikeDeploying.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<DeployableStorageDeployingEventArgs> DeployableStorageDeploying;

        public static void OnDeployableStorageDeploying(DeployableStorageDeployingEventArgs ev) => DeployableStorageDeploying.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<LEDLightDeployingEventArgs> LEDLightDeploying;

        public static void OnLEDLightDeploying(LEDLightDeployingEventArgs ev) => LEDLightDeploying.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BeaconDeployingEventArgs> BeaconDeploying;

        public static void OnBeaconDeploying(BeaconDeployingEventArgs ev) => BeaconDeploying.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<FlareDeployingEventArgs> FlareDeploying;

        public static void OnFlareDeploying(FlareDeployingEventArgs ev) => FlareDeploying.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ThumperDeployingEventArgs> ThumperDeploying;

        public static void OnThumperDeploying(ThumperDeployingEventArgs ev) => ThumperDeploying.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<TeleportationToolUsedEventArgs> TeleportationToolUsed;

        public static void OnTeleportationToolUsed(TeleportationToolUsedEventArgs ev) => TeleportationToolUsed.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BeaconLabelChangedEventArgs> BeaconLabelChanged;

        public static void OnBeaconLabelChanged(BeaconLabelChangedEventArgs ev) => BeaconLabelChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SpyPenguinDeployingEventArgs> SpyPenguinDeploying;

        public static void OnSpyPenguinDeploying(SpyPenguinDeployingEventArgs ev) => SpyPenguinDeploying.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SpyPenguinItemPickedUpEventArgs> SpyPenguinItemPickedUp;

        public static void OnSpyPenguinItemPickedUp(SpyPenguinItemPickedUpEventArgs ev) => SpyPenguinItemPickedUp.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SpyPenguinSnowStalkerInteractingEventArgs> SpyPenguinSnowStalkerInteracting;

        public static void OnSpyPenguinSnowStalkerInteracting(SpyPenguinSnowStalkerInteractingEventArgs ev) => SpyPenguinSnowStalkerInteracting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SpyPenguinItemGrabingEventArgs> SpyPenguinItemGrabing;

        public static void OnSpyPenguinItemGrabing(SpyPenguinItemGrabingEventArgs ev) => SpyPenguinItemGrabing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<WeldingEventArgs> Welding;

        public static void OnWelding(WeldingEventArgs ev) => Welding.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<DroneCameraDeployingEventArgs> DroneCameraDeploying;

        public static void OnDroneCameraDeploying(DroneCameraDeployingEventArgs ev) => DroneCameraDeploying.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PipeSurfaceFloaterDeployingEventArgs> PipeSurfaceFloaterDeploying;

        public static void OnPipeSurfaceFloaterDeploying(PipeSurfaceFloaterDeployingEventArgs ev) => PipeSurfaceFloaterDeploying.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<OxygenPipePlacingEventArgs> OxygenPipePlacing;

        public static void OnOxygenPipePlacing(OxygenPipePlacingEventArgs ev) => OxygenPipePlacing.CustomInvoke(ev);
    }
}
