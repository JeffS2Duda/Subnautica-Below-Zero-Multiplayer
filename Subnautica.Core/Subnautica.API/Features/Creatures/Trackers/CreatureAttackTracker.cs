namespace Subnautica.API.Features.Creatures.Trackers
{
    public class CreatureAttackTracker : BaseAnimationTracker
    {
        private string Animation { get; set; } = "attack";

        public override bool OnTrackerChecking(Creature creature, byte oldValue, out byte result)
        {
            result = 0;

            var attack = creature.GetAnimator().GetBool(this.Animation) ? 1 : 0;
            if (attack != oldValue)
            {
                if (attack == 1)
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
