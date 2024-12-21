namespace Subnautica.API.Features.Creatures.Trackers
{
    using System.Collections.Generic;

    public abstract class BaseAnimationTracker
    {
        public virtual List<byte> AllowedCustomResults { get; set; } = new List<byte>();

        public abstract bool OnTrackerChecking(global::Creature creature, byte oldValue, out byte result);

        public abstract void OnTrackerExecuting(global::Creature creature, byte result);
    }
}
