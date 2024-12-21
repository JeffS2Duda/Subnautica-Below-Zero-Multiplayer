namespace Subnautica.Events.Handlers
{
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class Vehicle
    {
        public static event SubnauticaPluginEventHandler<UpgradeConsoleOpeningEventArgs> UpgradeConsoleOpening;

        public static void OnUpgradeConsoleOpening(UpgradeConsoleOpeningEventArgs ev) => UpgradeConsoleOpening.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<UpgradeConsoleModuleAddedEventArgs> UpgradeConsoleModuleAdded;

        public static void OnUpgradeConsoleModuleAdded(UpgradeConsoleModuleAddedEventArgs ev) => UpgradeConsoleModuleAdded.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<UpgradeConsoleModuleRemovedEventArgs> UpgradeConsoleModuleRemoved;

        public static void OnUpgradeConsoleModuleRemoved(UpgradeConsoleModuleRemovedEventArgs ev) => UpgradeConsoleModuleRemoved.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<VehicleEnteringEventArgs> Entering;

        public static void OnEntering(VehicleEnteringEventArgs ev) => Entering.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<VehicleInteriorToggleEventArgs> InteriorToggle;

        public static void OnInteriorToggle(VehicleInteriorToggleEventArgs ev) => InteriorToggle.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<VehicleExitedEventArgs> Exited;

        public static void OnExited(VehicleExitedEventArgs ev) => Exited.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<VehicleUpdatedEventArgs> Updated;

        public static void OnUpdated(VehicleUpdatedEventArgs ev) => Updated.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<LightChangedEventArgs> LightChanged;

        public static void OnLightChanged(LightChangedEventArgs ev) => LightChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SeaTruckConnectingEventArgs> SeaTruckConnecting;

        public static void OnSeaTruckConnecting(SeaTruckConnectingEventArgs ev) => SeaTruckConnecting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ExosuitJumpingEventArgs> ExosuitJumping;

        public static void OnExosuitJumping(ExosuitJumpingEventArgs ev) => ExosuitJumping.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SeaTruckDetachingEventArgs> SeaTruckDetaching;

        public static void OnSeaTruckDetaching(SeaTruckDetachingEventArgs ev) => SeaTruckDetaching.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ExosuitItemPickedUpEventArgs> ExosuitItemPickedUp;

        public static void OnExosuitItemPickedUp(ExosuitItemPickedUpEventArgs ev) => ExosuitItemPickedUp.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ExosuitDrillingEventArgs> ExosuitDrilling;

        public static void OnExosuitDrilling(ExosuitDrillingEventArgs ev) => ExosuitDrilling.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<VehicleDockingEventArgs> Docking;

        public static void OnDocking(VehicleDockingEventArgs ev) => Docking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<VehicleUndockingEventArgs> Undocking;

        public static void OnUndocking(VehicleUndockingEventArgs ev) => Undocking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SeaTruckPictureFrameOpeningEventArgs> SeaTruckPictureFrameOpening;

        public static void OnSeaTruckPictureFrameOpening(SeaTruckPictureFrameOpeningEventArgs ev) => SeaTruckPictureFrameOpening.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SeaTruckPictureFrameImageSelectingEventArgs> SeaTruckPictureFrameImageSelecting;

        public static void OnSeaTruckPictureFrameImageSelecting(SeaTruckPictureFrameImageSelectingEventArgs ev) => SeaTruckPictureFrameImageSelecting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<MapRoomCameraDockingEventArgs> MapRoomCameraDocking;

        public static void OnMapRoomCameraDocking(MapRoomCameraDockingEventArgs ev) => MapRoomCameraDocking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<SeaTruckModuleInitializedEventArgs> SeaTruckModuleInitialized;

        public static void OnSeaTruckModuleInitialized(SeaTruckModuleInitializedEventArgs ev) => SeaTruckModuleInitialized.CustomInvoke(ev);
    }
}
