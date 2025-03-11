namespace Subnautica.Events.Handlers
{
    using Subnautica.API.Extensions;
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class Furnitures
    {
        public static event SubnauticaPluginEventHandler<ToiletSwitchToggleEventArgs> ToiletSwitchToggle;

        public static void OnToiletSwitchToggle(ToiletSwitchToggleEventArgs ev) => ToiletSwitchToggle.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<EmmanuelPendulumSwitchToggleEventArgs> EmmanuelPendulumSwitchToggle;

        public static void OnEmmanuelPendulumSwitchToggle(EmmanuelPendulumSwitchToggleEventArgs ev) => EmmanuelPendulumSwitchToggle.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<AromatherapyLampSwitchToggleEventArgs> AromatherapyLampSwitchToggle;

        public static void OnAromatherapyLampSwitchToggle(AromatherapyLampSwitchToggleEventArgs ev) => AromatherapyLampSwitchToggle.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SmallStoveSwitchToggleEventArgs> SmallStoveSwitchToggle;

        public static void OnSmallStoveSwitchToggle(SmallStoveSwitchToggleEventArgs ev) => SmallStoveSwitchToggle.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SinkSwitchToggleEventArgs> SinkSwitchToggle;

        public static void OnSinkSwitchToggle(SinkSwitchToggleEventArgs ev) => SinkSwitchToggle.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ShowerSwitchToggleEventArgs> ShowerSwitchToggle;

        public static void OnShowerSwitchToggle(ShowerSwitchToggleEventArgs ev) => ShowerSwitchToggle.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SnowmanDestroyingEventArgs> SnowmanDestroying;

        public static void OnSnowmanDestroying(SnowmanDestroyingEventArgs ev) => SnowmanDestroying.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SignDataChangedEventArgs> SignDataChanged;

        public static void OnSignDataChanged(SignDataChangedEventArgs ev) => SignDataChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<JukeboxUsedEventArgs> JukeboxUsed;

        public static void OnJukeboxUsed(JukeboxUsedEventArgs ev) => JukeboxUsed.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PictureFrameImageSelectingEventArgs> PictureFrameImageSelecting;

        public static void OnPictureFrameImageSelecting(PictureFrameImageSelectingEventArgs ev) => PictureFrameImageSelecting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BedIsCanSleepCheckingEventArgs> BedIsCanSleepChecking;

        public static void OnBedIsCanSleepChecking(BedIsCanSleepCheckingEventArgs ev) => BedIsCanSleepChecking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BedEnterInUseModeEventArgs> BedEnterInUseMode;

        public static void OnBedEnterInUseMode(BedEnterInUseModeEventArgs ev) => BedEnterInUseMode.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<AquariumDataChangedEventArgs> AquariumDataChanged;

        public static void OnAquariumDataChanged(AquariumDataChangedEventArgs ev) => AquariumDataChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CrafterOpeningEventArgs> CrafterOpening;

        public static void OnCrafterOpening(CrafterOpeningEventArgs ev) => CrafterOpening.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CrafterClosedEventArgs> CrafterClosed;

        public static void OnCrafterClosed(CrafterClosedEventArgs ev) => CrafterClosed.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CrafterEndedEventArgs> CrafterEnded;

        public static void OnCrafterEnded(CrafterEndedEventArgs ev) => CrafterEnded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CrafterBeginEventArgs> CrafterBegin;

        public static void OnCrafterBegin(CrafterBeginEventArgs ev) => CrafterBegin.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CrafterItemPickupEventArgs> CrafterItemPickup;

        public static void OnCrafterItemPickup(CrafterItemPickupEventArgs ev) => CrafterItemPickup.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BenchStandupEventArgs> BenchStandup;

        public static void OnBenchStandup(BenchStandupEventArgs ev) => BenchStandup.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BenchSitdownEventArgs> BenchSitdown;

        public static void OnBenchSitdown(BenchSitdownEventArgs ev) => BenchSitdown.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SignSelectEventArgs> SignSelect;

        public static void OnSignSelect(SignSelectEventArgs ev) => SignSelect.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SignDeselectEventArgs> SignDeselect;

        public static void OnSignDeselect(SignDeselectEventArgs ev) => SignDeselect.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PictureFrameOpeningEventArgs> PictureFrameOpening;

        public static void OnPictureFrameOpening(PictureFrameOpeningEventArgs ev) => PictureFrameOpening.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ChargerOpeningEventArgs> ChargerOpening;

        public static void OnChargerOpening(ChargerOpeningEventArgs ev) => ChargerOpening.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<HoverpadHoverbikeSpawningEventArgs> HoverpadHoverbikeSpawning;

        public static void OnHoverpadHoverbikeSpawning(HoverpadHoverbikeSpawningEventArgs ev) => HoverpadHoverbikeSpawning.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<RecyclotronRecycleEventArgs> RecyclotronRecycle;

        public static void OnRecyclotronRecycle(RecyclotronRecycleEventArgs ev) => RecyclotronRecycle.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<FiltrationMachineOpeningEventArgs> FiltrationMachineOpening;

        public static void OnFiltrationMachineOpening(FiltrationMachineOpeningEventArgs ev) => FiltrationMachineOpening.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlanterItemAddedEventArgs> PlanterItemAdded;

        public static void OnPlanterItemAdded(PlanterItemAddedEventArgs ev) => PlanterItemAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlanterGrownedEventArgs> PlanterGrowned;

        public static void OnPlanterGrowned(PlanterGrownedEventArgs ev) => PlanterGrowned.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlanterProgressCompletedEventArgs> PlanterProgressCompleted;

        public static void OnPlanterProgressCompleted(PlanterProgressCompletedEventArgs ev) => PlanterProgressCompleted.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BedExitInUseModeEventArgs> BedExitInUseMode;

        public static void OnBedExitInUseMode(BedExitInUseModeEventArgs ev) => BedExitInUseMode.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BulkheadOpeningEventArgs> BulkheadOpening;

        public static void OnBulkheadOpening(BulkheadOpeningEventArgs ev) => BulkheadOpening.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BulkheadClosingEventArgs> BulkheadClosing;

        public static void OnBulkheadClosing(BulkheadClosingEventArgs ev) => BulkheadClosing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseControlRoomMinimapUsingEventArgs> BaseControlRoomMinimapUsing;

        public static void OnBaseControlRoomMinimapUsing(BaseControlRoomMinimapUsingEventArgs ev) => BaseControlRoomMinimapUsing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseControlRoomMinimapExitingEventArgs> BaseControlRoomMinimapExiting;

        public static void OnBaseControlRoomMinimapExiting(BaseControlRoomMinimapExitingEventArgs ev) => BaseControlRoomMinimapExiting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseControlRoomCellPowerChangingEventArgs> BaseControlRoomCellPowerChanging;

        public static void OnBaseControlRoomCellPowerChanging(BaseControlRoomCellPowerChangingEventArgs ev) => BaseControlRoomCellPowerChanging.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseControlRoomMinimapMovingEventArgs> BaseControlRoomMinimapMoving;

        public static void OnBaseControlRoomMinimapMoving(BaseControlRoomMinimapMovingEventArgs ev) => BaseControlRoomMinimapMoving.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<HoverpadDockingEventArgs> HoverpadDocking;

        public static void OnHoverpadDocking(HoverpadDockingEventArgs ev) => HoverpadDocking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<HoverpadUnDockingEventArgs> HoverpadUnDocking;

        public static void OnHoverpadUnDocking(HoverpadUnDockingEventArgs ev) => HoverpadUnDocking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<HoverpadShowroomTriggeringEventArgs> HoverpadShowroomTriggering;

        public static void OnHoverpadShowroomTriggering(HoverpadShowroomTriggeringEventArgs ev) => HoverpadShowroomTriggering.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SpotLightInitializedEventArgs> SpotLightInitialized;

        public static void OnSpotLightInitialized(SpotLightInitializedEventArgs ev) => SpotLightInitialized.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<TechLightInitializedEventArgs> TechLightInitialized;

        public static void OnTechLightInitialized(TechLightInitializedEventArgs ev) => TechLightInitialized.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseMapRoomScanStoppingEventArgs> BaseMapRoomScanStopping;

        public static void OnBaseMapRoomScanStopping(BaseMapRoomScanStoppingEventArgs ev) => BaseMapRoomScanStopping.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseMapRoomScanStartingEventArgs> BaseMapRoomScanStarting;

        public static void OnBaseMapRoomScanStarting(BaseMapRoomScanStartingEventArgs ev) => BaseMapRoomScanStarting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<MapRoomCameraChangingEventArgs> BaseMapRoomCameraChanging;

        public static void OnBaseMapRoomCameraChanging(MapRoomCameraChangingEventArgs ev) => BaseMapRoomCameraChanging.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseMapRoomResourceDiscoveringEventArgs> BaseMapRoomResourceDiscovering;

        public static void OnBaseMapRoomResourceDiscovering(BaseMapRoomResourceDiscoveringEventArgs ev) => BaseMapRoomResourceDiscovering.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseMapRoomInitializedEventArgs> BaseMapRoomInitialized;

        public static void OnBaseMapRoomInitialized(BaseMapRoomInitializedEventArgs ev) => BaseMapRoomInitialized.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseMoonpoolExpansionUndockingTimelineCompletingEventArgs> BaseMoonpoolExpansionUndockingTimelineCompleting;

        public static void OnBaseMoonpoolExpansionUndockingTimelineCompleting(BaseMoonpoolExpansionUndockingTimelineCompletingEventArgs ev) => BaseMoonpoolExpansionUndockingTimelineCompleting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseMoonpoolExpansionDockingTimelineCompletingEventArgs> BaseMoonpoolExpansionDockingTimelineCompleting;

        public static void OnBaseMoonpoolExpansionDockingTimelineCompleting(BaseMoonpoolExpansionDockingTimelineCompletingEventArgs ev) => BaseMoonpoolExpansionDockingTimelineCompleting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseMoonpoolExpansionDockTailEventArgs> BaseMoonpoolExpansionDockTail;

        public static void OnBaseMoonpoolExpansionDockTail(BaseMoonpoolExpansionDockTailEventArgs ev) => BaseMoonpoolExpansionDockTail.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseMoonpoolExpansionUndockTailEventArgs> BaseMoonpoolExpansionUndockTail;

        public static void OnBaseMoonpoolExpansionUndockTail(BaseMoonpoolExpansionUndockTailEventArgs ev) => BaseMoonpoolExpansionUndockTail.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlanterStorageResetingEventArgs> PlanterStorageReseting;

        public static void OnPlanterStorageReseting(PlanterStorageResetingEventArgs ev) => PlanterStorageReseting.CustomInvoke(ev);
    }
}
