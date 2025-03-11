using UnityEngine;

namespace Subnautica.API.MonoBehaviours;

public class PlanterItemComponent : MonoBehaviour
{
    public float Health { get; private set; } = -1f;

    public float TimeNextFruit { get; private set; } = 0f;

    public byte ActiveFruitCount { get; private set; } = 0;

    public float TimeStartGrowth { get; private set; } = 0f;

    public void SetHealth(float health)
    {
        this.Health = health;
        GrownPlant linkedGrownPlant = base.GetComponent<Plantable>().linkedGrownPlant;
        bool flag = linkedGrownPlant;
        if (flag)
        {
            linkedGrownPlant.GetComponent<LiveMixin>().health = health;
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