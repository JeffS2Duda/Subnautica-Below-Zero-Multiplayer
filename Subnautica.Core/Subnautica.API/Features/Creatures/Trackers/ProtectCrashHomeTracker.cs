namespace Subnautica.API.Features.Creatures.Trackers
{
    using Subnautica.API.Extensions;

    public class ProtectCrashHomeTracker : BaseAnimationTracker
    {
        public override bool OnTrackerChecking(Creature creature, byte oldValue, out byte result)
        {
            result = 0;

            var agressionValue = creature.Aggression.Value.ToByte();
            if (agressionValue <= 30)
            {
                if (oldValue != 0)
                {
                    result = 0;
                    return true;
                }
            }
            else if (agressionValue <= 80)
            {
                if (oldValue != 55)
                {
                    result = 55;
                    return true;
                }
            }
            else if (agressionValue < 100)
            {
                if (oldValue != 90)
                {
                    result = 90;
                    return true;
                }
            }
            else
            {
                if (oldValue != 100)
                {
                    result = 100;
                    return true;
                }
            }

            return false;
        }

        public override void OnTrackerExecuting(Creature creature, byte result)
        {
            creature.GetAnimator().SetFloat(Creature.animAggressive, result.ToFloat());
        }
    }
}
