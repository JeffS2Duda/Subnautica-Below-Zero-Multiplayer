namespace Subnautica.Client.MonoBehaviours.Construction
{
    using System.Collections.Generic;

    using UnityEngine;

    public class BuilderBaseHullStrength : MonoBehaviour
    {
        private BaseFloodSim BaseFloodSim { get; set; }

        private Dictionary<ushort, CellWaterLevelItem> TargetWaterLevels { get; set; } = new Dictionary<ushort, CellWaterLevelItem>();

        private int TargetLevelCount { get; set; } = 0;

        private HashSet<ushort> ActiveLevels { get; set; } = new HashSet<ushort>();

        private HashSet<ushort> RemoveToActiveLevels { get; set; } = new HashSet<ushort>();

        public void Awake()
        {
            this.BaseFloodSim = this.GetComponent<BaseFloodSim>();
        }

        public void ChangeCellWaterLevelSize(int size)
        {
            if (size > this.TargetLevelCount)
            {
                for (ushort i = (ushort)this.TargetWaterLevels.Count; i <= size; i++)
                {
                    this.TargetWaterLevels[i] = new CellWaterLevelItem();
                }

                this.TargetLevelCount = this.TargetWaterLevels.Count;
            }
        }

        public void SetCellWaterLevel(ushort index, float value)
        {
            this.ChangeCellWaterLevelSize(index + 2);

            this.TargetWaterLevels[index].Reset();
            this.TargetWaterLevels[index].Start(index, value);

            this.ActiveLevels.Add(index);
        }

        public void Update()
        {
            if (this.ActiveLevels.Count > 0)
            {
                foreach (var index in this.ActiveLevels)
                {
                    var currentWaterLevel = this.GetBaseCellWaterLevel(index);
                    if (currentWaterLevel == -1f)
                    {
                        continue;
                    }

                    var item = this.TargetWaterLevels[index];
                    if (item.IsInitialized == false)
                    {
                        item.SetCurrentValue(currentWaterLevel);
                    }

                    item.InterpolateWater(Time.unscaledDeltaTime);

                    this.SetBaseCellWaterLevel(index, item.GetValue());

                    if (item.IsFinished())
                    {
                        this.RemoveToActiveLevels.Add(index);
                    }
                }

                if (this.RemoveToActiveLevels.Count > 0)
                {
                    foreach (var index in this.RemoveToActiveLevels)
                    {
                        this.ActiveLevels.Remove(index);
                    }

                    this.RemoveToActiveLevels.Clear();
                }
            }
        }

        private void SetBaseCellWaterLevel(ushort index, float waterLevel)
        {
            this.BaseFloodSim.cellWaterLevel[index] = waterLevel;
        }

        private float GetBaseCellWaterLevel(ushort index)
        {
            return index < this.BaseFloodSim.cellWaterLevel.Length ? this.BaseFloodSim.cellWaterLevel[index] : -1f;
        }

        public void OnDestroy()
        {
            this.ActiveLevels.Clear();
            this.TargetWaterLevels.Clear();
            this.RemoveToActiveLevels.Clear();
        }
    }

    public class CellWaterLevelItem
    {
        public ushort Index { get; set; }

        public float CurrentValue { get; set; }

        public float TargetValue { get; set; }

        public float InterpolateValue { get; set; }

        public CellWaterLevelItem()
        {
            this.Reset();
        }

        public bool IsInitialized
        {
            get
            {
                return this.CurrentValue != -1f;
            }
        }

        public void Start(ushort index, float targetValue)
        {
            this.Index = index;
            this.TargetValue = targetValue;
        }

        public void SetCurrentValue(float currentValue)
        {
            this.CurrentValue = currentValue;
        }

        public void InterpolateWater(float deltaTime)
        {
            this.InterpolateValue += deltaTime;
        }

        public bool IsFinished()
        {
            return this.InterpolateValue >= 1f;
        }

        public float GetValue()
        {
            return Mathf.Lerp(this.CurrentValue, this.TargetValue, this.InterpolateValue);
        }

        public void Reset()
        {
            this.Index = 0;
            this.CurrentValue = -1f;
            this.TargetValue = 0f;
            this.InterpolateValue = 0f;
        }
    }
}


