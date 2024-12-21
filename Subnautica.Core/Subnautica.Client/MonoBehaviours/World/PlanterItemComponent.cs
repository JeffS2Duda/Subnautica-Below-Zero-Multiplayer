namespace Subnautica.Client.MonoBehaviours.World
{
    using UnityEngine;

    public class PlanterItemComponent : MonoBehaviour
    {
        public float Health { get; private set; } = -1f;

        public float TimeNextFruit { get; private set; } = 0f;

        public byte ActiveFruitCount { get; private set; } = 0;

        public float TimeStartGrowth { get; private set; } = 0f;

        public void SetHealth(float health)
        {
            this.Health = health;

            var grownPlant = this.GetComponent<Plantable>().linkedGrownPlant;
            if (grownPlant)
            {
                grownPlant.GetComponent<global::LiveMixin>().health = health;
            }
        }

        public void SetTimeNextFruit(float timeNextFruit)
        {
            this.TimeNextFruit = timeNextFruit;
        }

        public void SetActiveFruitCount(byte activeFruitCount)
        {
            this.ActiveFruitCount = activeFruitCount;
        }

        public void SetStartingTime(float time)
        {
            this.TimeStartGrowth = time;
        }
    }
}
