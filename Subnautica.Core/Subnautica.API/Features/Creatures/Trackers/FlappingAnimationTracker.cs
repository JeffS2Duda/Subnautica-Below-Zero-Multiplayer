namespace Subnautica.API.Features.Creatures.Trackers
{
    public class FlappingAnimationTracker : BaseAnimationTracker
    {
        private string Animation { get; set; } = "flapping";

        public override bool OnTrackerChecking(Creature creature, byte oldValue, out byte result)
        {
            result = 0;

            var flapping = creature.GetAnimator().GetBool(this.Animation) ? 1 : 0;
            if (flapping != oldValue)
            {
                if (flapping == 1)
                {
                    result = 1;
                }
                else
                {
                    result = 0;
                }

                return true;
            }

            return false;
        }

        public override void OnTrackerExecuting(Creature creature, byte result)
        {
            creature.GetAnimator().SetBool(this.Animation, result == 1);
        }
    }
}
