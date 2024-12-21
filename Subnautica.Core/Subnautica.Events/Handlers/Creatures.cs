namespace Subnautica.Events.Handlers
{
    using Subnautica.Events.EventArgs;

    using static Subnautica.API.Extensions.EventExtensions;

    public class Creatures
    {
        public static event SubnauticaPluginEventHandler<CreatureEnabledEventArgs> Enabled;

        public static void OnEnabled(CreatureEnabledEventArgs ev) => Enabled.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CreatureDisabledEventArgs> Disabled;

        public static void OnDisabled(CreatureDisabledEventArgs ev) => Disabled.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CreatureFreezingEventArgs> Freezing;

        public static void OnFreezing(CreatureFreezingEventArgs ev) => Freezing.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CreatureMeleeAttackingEventArgs> MeleeAttacking;

        public static void OnMeleeAttacking(CreatureMeleeAttackingEventArgs ev) => MeleeAttacking.CustomInvoke(ev);
        
        public static event SubnauticaPluginEventHandler<CreatureAttackLastTargetStoppedEventArgs> CreatureAttackLastTargetStopped;

        public static void OnCreatureAttackLastTargetStopped(CreatureAttackLastTargetStoppedEventArgs ev) => CreatureAttackLastTargetStopped.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CreatureLeviathanMeleeAttackingEventArgs> LeviathanMeleeAttacking;

        public static void OnLeviathanMeleeAttacking(CreatureLeviathanMeleeAttackingEventArgs ev) => LeviathanMeleeAttacking.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CreatureAttackLastTargetStartingEventArgs> CreatureAttackLastTargetStarting;

        public static void OnCreatureAttackLastTargetStarting(CreatureAttackLastTargetStartingEventArgs ev) => CreatureAttackLastTargetStarting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CreatureCallSoundTriggeringEventArgs> CallSoundTriggering;

        public static void OnCallSoundTriggering(CreatureCallSoundTriggeringEventArgs ev) => CallSoundTriggering.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<GlowWhaleSFXTriggeredEventArgs> GlowWhaleSFXTriggered;

        public static void OnGlowWhaleSFXTriggered(GlowWhaleSFXTriggeredEventArgs ev) => GlowWhaleSFXTriggered.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<GlowWhaleRideStartingEventArgs> GlowWhaleRideStarting;

        public static void OnGlowWhaleRideStarting(GlowWhaleRideStartingEventArgs ev) => GlowWhaleRideStarting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<GlowWhaleRideStopedEventArgs> GlowWhaleRideStoped;

        public static void OnGlowWhaleRideStoped(GlowWhaleRideStopedEventArgs ev) => GlowWhaleRideStoped.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<GlowWhaleEyeCinematicStartingEventArgs> GlowWhaleEyeCinematicStarting;

        public static void OnGlowWhaleEyeCinematicStarting(GlowWhaleEyeCinematicStartingEventArgs ev) => GlowWhaleEyeCinematicStarting.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CreatureAnimationChangedEventArgs> AnimationChanged;

        public static void OnAnimationChanged(CreatureAnimationChangedEventArgs ev) => AnimationChanged.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<CrashFishInflatingEventArgs> CrashFishInflating;

        public static void OnCrashFishInflating(CrashFishInflatingEventArgs ev) => CrashFishInflating.CustomInvoke(ev);

        public static event SubnauticaPluginEventHandler<LilyPaddlerHypnotizeStartingEventArgs> LilyPaddlerHypnotizeStarting;

        public static void OnLilyPaddlerHypnotizeStarting(LilyPaddlerHypnotizeStartingEventArgs ev) => LilyPaddlerHypnotizeStarting.CustomInvoke(ev);
    }
}
