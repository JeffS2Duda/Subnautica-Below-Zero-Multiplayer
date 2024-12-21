namespace Subnautica.Events.Handlers
{
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class Building
    {
        public static event SubnauticaPluginEventHandler<ConstructionGhostMovedEventArgs> ConstructingGhostMoved;

        public static void OnConstructingGhostMoved(ConstructionGhostMovedEventArgs ev) => ConstructingGhostMoved.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ConstructionGhostTryPlacingEventArgs> ConstructingGhostTryPlacing;

        public static void OnConstructingGhostTryPlacing(ConstructionGhostTryPlacingEventArgs ev) => ConstructingGhostTryPlacing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ConstructionAmountChangedEventArgs> ConstructingAmountChanged;

        public static void OnConstructingAmountChanged(ConstructionAmountChangedEventArgs ev) => ConstructingAmountChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<ConstructionCompletedEventArgs> ConstructingCompleted;

        public static void OnConstructingCompleted(ConstructionCompletedEventArgs ev) => ConstructingCompleted.CustomInvoke(ev);
        public static event SubnauticaPluginEventHandler<ConstructionRemovedEventArgs> ConstructingRemoved;

        public static void OnConstructingRemoved(ConstructionRemovedEventArgs ev) => ConstructingRemoved.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<DeconstructionBeginEventArgs> DeconstructionBegin;

        public static void OnDeconstructionBegin(DeconstructionBeginEventArgs ev) => DeconstructionBegin.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<FurnitureDeconstructionBeginEventArgs> FurnitureDeconstructionBegin;

        public static void OnFurnitureDeconstructionBegin(FurnitureDeconstructionBeginEventArgs ev) => FurnitureDeconstructionBegin.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<BaseHullStrengthCrushingEventArgs> BaseHullStrengthCrushing;

        public static void OnBaseHullStrengthCrushing(BaseHullStrengthCrushingEventArgs ev) => BaseHullStrengthCrushing.CustomInvoke(ev);
    }
}
