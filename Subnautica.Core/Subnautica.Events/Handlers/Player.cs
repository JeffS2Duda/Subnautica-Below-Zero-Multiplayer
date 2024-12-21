namespace Subnautica.Events.Handlers
{
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class Player
    {
        public static event SubnauticaPluginEventHandler<PlayerUpdatedEventArgs> Updated;

        public static void OnUpdated(PlayerUpdatedEventArgs ev) => Updated.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerStatsUpdatedEventArgs> StatsUpdated;

        public static void OnStatsUpdated(PlayerStatsUpdatedEventArgs ev) => StatsUpdated.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerBaseEnteredEventArgs> PlayerBaseEntered;

        public static void OnPlayerBaseEntered(PlayerBaseEnteredEventArgs ev) => PlayerBaseEntered.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerBaseExitedEventArgs> PlayerBaseExited;

        public static void OnPlayerBaseExited(PlayerBaseExitedEventArgs ev) => PlayerBaseExited.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ItemDrawedEventArgs> ItemDrawed;

        public static void OnItemDrawed(ItemDrawedEventArgs ev) => ItemDrawed.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ItemActionStartedEventArgs> ItemActionStarted;

        public static void OnItemActionStarted(ItemActionStartedEventArgs ev) => ItemActionStarted.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ItemFirstUseAnimationStopedEventArgs> ItemFirstUseAnimationStoped;

        public static void OnItemFirstUseAnimationStoped(ItemFirstUseAnimationStopedEventArgs ev) => ItemFirstUseAnimationStoped.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<EntityScannerCompletedEventArgs> EntityScannerCompleted;

        public static void OnEntityScannerCompleted(EntityScannerCompletedEventArgs ev) => EntityScannerCompleted.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerItemPickedUpEventArgs> ItemPickedUp;

        public static void OnItemPickedUp(PlayerItemPickedUpEventArgs ev) => ItemPickedUp.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerAnimationChangedEventArgs> AnimationChanged;

        public static void OnAnimationChanged(PlayerAnimationChangedEventArgs ev) => AnimationChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerItemDropingEventArgs> ItemDroping;

        public static void OnItemDroping(PlayerItemDropingEventArgs ev) => ItemDroping.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler SleepScreenStopingStarted;

        public static void OnSleepScreenStopingStarted() => SleepScreenStopingStarted.CustomInvoke();

        public static event SubnauticaPluginEventHandler SleepScreenStartingCompleted;

        public static void OnSleepScreenStartingCompleted() => SleepScreenStartingCompleted.CustomInvoke();

        public static event SubnauticaPluginEventHandler<UseableDiveHatchClickingEventArgs> UseableDiveHatchClicking;

        public static void OnUseableDiveHatchClicking(UseableDiveHatchClickingEventArgs ev) => UseableDiveHatchClicking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerEnteredInteriorEventArgs> EnteredInterior;

        public static void OnEnteredInterior(PlayerEnteredInteriorEventArgs ev) => EnteredInterior.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerExitedInteriorEventArgs> ExitedInterior;

        public static void OnExitedInterior(PlayerExitedInteriorEventArgs ev) => ExitedInterior.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerClimbingEventArgs> Climbing;

        public static void OnClimbing(PlayerClimbingEventArgs ev) => Climbing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerDeadEventArgs> Dead;

        public static void OnDead(PlayerDeadEventArgs ev) => Dead.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler Spawned;

        public static void OnSpawned() => Spawned.CustomInvoke();

        public static event SubnauticaPluginEventHandler<EnergyMixinClickingEventArgs> EnergyMixinClicking;

        public static void OnEnergyMixinClicking(EnergyMixinClickingEventArgs ev) => EnergyMixinClicking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<EnergyMixinSelectingEventArgs> EnergyMixinSelecting;

        public static void OnEnergyMixinSelecting(EnergyMixinSelectingEventArgs ev) => EnergyMixinSelecting.CustomInvoke(ev);


        public static event SubnauticaPluginEventHandler<EnergyMixinClosedEventArgs> EnergyMixinClosed;

        public static void OnEnergyMixinClosed(EnergyMixinClosedEventArgs ev) => EnergyMixinClosed.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BreakableResourceBreakingEventArgs> BreakableResourceBreaking;

        public static void OnBreakableResourceBreaking(BreakableResourceBreakingEventArgs ev) => BreakableResourceBreaking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerPingVisibilityChangedEventArgs> PingVisibilityChanged;

        public static void OnPingVisibilityChanged(PlayerPingVisibilityChangedEventArgs ev) => PingVisibilityChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerPingColorChangedEventArgs> PingColorChanged;

        public static void OnPingColorChanged(PlayerPingColorChangedEventArgs ev) => PingColorChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler PrecursorTeleporterUsed;

        public static void OnPrecursorTeleporterUsed() => PrecursorTeleporterUsed.CustomInvoke();

        public static event SubnauticaPluginEventHandler PrecursorTeleportationCompleted;

        public static void OnPrecursorTeleportationCompleted() => PrecursorTeleportationCompleted.CustomInvoke();

        public static event SubnauticaPluginEventHandler<ToolBatteryEnergyChangedEventArgs> ToolBatteryEnergyChanged;

        public static void OnToolBatteryEnergyChanged(ToolBatteryEnergyChangedEventArgs ev) => ToolBatteryEnergyChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerUsingCommandEventArgs> PlayerUsingCommand;

        public static void OnPlayerUsingCommand(PlayerUsingCommandEventArgs ev) => PlayerUsingCommand.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerRespawnPointChangedEventArgs> RespawnPointChanged;

        public static void OnRespawnPointChanged(PlayerRespawnPointChangedEventArgs ev) => RespawnPointChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<PlayerFreezedEventArgs> Freezed;

        public static void OnFreezed(PlayerFreezedEventArgs ev) => Freezed.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler Unfreezed;

        public static void OnUnfreezed() => Unfreezed.CustomInvoke();
    }
}
