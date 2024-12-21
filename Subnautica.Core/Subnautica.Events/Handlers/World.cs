namespace Subnautica.Events.Handlers
{
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class World
    {
        public static event SubnauticaPluginEventHandler<ThermalLilyRangeCheckingEventArgs> ThermalLilyRangeChecking;

        public static void OnThermalLilyRangeChecking(ThermalLilyRangeCheckingEventArgs ev) => ThermalLilyRangeChecking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ThermalLilyAnimationAnglesCheckingEventArgs> ThermalLilyAnimationAnglesChecking;

        public static void OnThermalLilyAnimationAnglesChecking(ThermalLilyAnimationAnglesCheckingEventArgs ev) => ThermalLilyAnimationAnglesChecking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<OxygenPlantClickingEventArgs> OxygenPlantClicking;

        public static void OnOxygenPlantClicking(OxygenPlantClickingEventArgs ev) => OxygenPlantClicking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<EntitySpawningEventArgs> EntitySpawning;

        public static void OnEntitySpawning(EntitySpawningEventArgs ev) => EntitySpawning.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<AlterraPdaPickedUpEventArgs> AlterraPdaPickedUp;

        public static void OnAlterraPdaPickedUp(AlterraPdaPickedUpEventArgs ev) => AlterraPdaPickedUp.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<JukeboxDiskPickedUpEventArgs> JukeboxDiskPickedUp;

        public static void OnJukeboxDiskPickedUp(JukeboxDiskPickedUpEventArgs ev) => JukeboxDiskPickedUp.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<EntitySpawnedEventArgs> EntitySpawned;

        public static void OnEntitySpawned(EntitySpawnedEventArgs ev) => EntitySpawned.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SupplyCrateOpenedEventArgs> SupplyCrateOpened;

        public static void OnSupplyCrateOpened(SupplyCrateOpenedEventArgs ev) => SupplyCrateOpened.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<DataboxItemPickedUpEventArgs> DataboxItemPickedUp;

        public static void OnDataboxItemPickedUp(DataboxItemPickedUpEventArgs ev) => DataboxItemPickedUp.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<TakeDamagingEventArgs> TakeDamaging;

        public static void OnTakeDamaging(TakeDamagingEventArgs ev) => TakeDamaging.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<FruitHarvestingEventArgs> FruitHarvesting;

        public static void OnFruitHarvesting(FruitHarvestingEventArgs ev) => FruitHarvesting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CellLoadingEventArgs> CellLoading;

        public static void OnCellLoading(CellLoadingEventArgs ev) => CellLoading.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CellUnLoadingEventArgs> CellUnLoading;

        public static void OnCellUnLoading(CellUnLoadingEventArgs ev) => CellUnLoading.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<GrownPlantHarvestingEventArgs> GrownPlantHarvesting;

        public static void OnGrownPlantHarvesting(GrownPlantHarvestingEventArgs ev) => GrownPlantHarvesting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<EntitySlotSpawningEventArgs> EntitySlotSpawning;

        public static void OnEntitySlotSpawning(EntitySlotSpawningEventArgs ev) => EntitySlotSpawning.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<LaserCutterEventArgs> LaserCutterUsing;

        public static void OnLaserCutterUsing(LaserCutterEventArgs ev) => LaserCutterUsing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SealedInitializedEventArgs> SealedInitialized;

        public static void OnSealedInitialized(SealedInitializedEventArgs ev) => SealedInitialized.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ElevatorCallingEventArgs> ElevatorCalling;

        public static void OnElevatorCalling(ElevatorCallingEventArgs ev) => ElevatorCalling.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SpawnOnKillingEventArgs> SpawnOnKilling;

        public static void OnSpawnOnKilling(SpawnOnKillingEventArgs ev) => SpawnOnKilling.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<WeatherProfileChangedEventArgs> WeatherProfileChanged;

        public static void OnWeatherProfileChanged(WeatherProfileChangedEventArgs ev) => WeatherProfileChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<TeleporterTerminalActivatingEventArgs> TeleporterTerminalActivating;

        public static void OnTeleporterTerminalActivating(TeleporterTerminalActivatingEventArgs ev) => TeleporterTerminalActivating.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<TeleporterInitializedEventArgs> TeleporterInitialized;

        public static void OnTeleporterInitialized(TeleporterInitializedEventArgs ev) => TeleporterInitialized.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ElevatorInitializedEventArgs> ElevatorInitialized;

        public static void OnElevatorInitialized(ElevatorInitializedEventArgs ev) => ElevatorInitialized.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CrushDamagingEventArgs> CrushDamaging;

        public static void OnCrushDamaging(CrushDamagingEventArgs ev) => CrushDamaging.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CosmeticItemPlacingEventArgs> CosmeticItemPlacing;

        public static void OnCosmeticItemPlacing(CosmeticItemPlacingEventArgs ev) => CosmeticItemPlacing.CustomInvoke(ev);
    }
}
