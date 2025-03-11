namespace Subnautica.API.Features.Creatures.MonoBehaviours.Shared
{
    using Subnautica.API.Extensions;
    using UnityEngine;

    public class MultiplayerWaterParkCreature : BaseMultiplayerCreature
    {
        public WaterParkCreature WaterParkCreature { get; set; }

        public StopwatchItem Timing { get; set; } = new StopwatchItem(1000f, null, true);

        public string CurrentWaterParkId { get; set; }

        public double AddedTime { get; set; }

        public void Awake()
        {
            this.WaterParkCreature = base.GetComponent<WaterParkCreature>();
        }

        public void OnEnable()
        {
            this.AddedTime = 0.0;
            this.CurrentWaterParkId = null;
            this.WaterParkCreature.age = 0f;
            this.WaterParkCreature.bornInside = true;
            this.WaterParkCreature.transform.localScale = Vector3.zero;
        }

        public void OnSpawned()
        {
            this.OnAddToWP();
        }

        public void OnChangedOwnership()
        {
            this.OnAddToWP();
        }

        public void OnAddToWP()
        {
            this.WaterParkCreature.CancelInvoke();
            this.WaterParkCreature.InitializeCreatureBornInWaterPark();
            this.WaterParkCreature.swimBehaviour = this.WaterParkCreature.gameObject.GetComponent<SwimBehaviour>();
            this.WaterParkCreature.GetComponent<Creature>().enabled = false;
            this.WaterParkCreature.InvokeRepeating("ValidatePosition", Random.value * 10f, 10f);
            bool isInside = this.WaterParkCreature.isInside;
            if (isInside)
            {
                this.WaterParkCreature.SetOutsideState();
            }
            this.WaterParkCreature.SetInsideState();
            bool flag = this.IsAdded();
            if (flag)
            {
                this.UpdateAge();
                this.UpdateLighting();
            }
            this.WaterParkCreature.gameObject.SendMessage("OnAddToWaterPark", this.WaterParkCreature, SendMessageOptions.DontRequireReceiver);
        }

        public void OnDisable()
        {
            this.WaterParkCreature.CancelInvoke();
            bool flag = this.IsAdded();
            if (flag)
            {
                this.WaterParkCreature.SetWaterPark(null);
            }
        }

        public void Update()
        {
            bool flag = base.MultiplayerCreature.CreatureItem.IsMine(0) && this.IsAdded();
            if (flag)
            {
                this.WaterParkCreature.UpdateMovement();
            }
            bool flag2 = this.Timing.IsFinished();
            if (flag2)
            {
                this.Timing.Restart();
                bool flag3 = this.IsAdded();
                string waterParkId = this.GetWaterParkId();
                bool flag4 = waterParkId != this.CurrentWaterParkId || !flag3;
                if (flag4)
                {
                    this.ChangeWaterPark(waterParkId);
                }
                bool flag5 = this.IsAdded();
                if (flag5)
                {
                    this.UpdateLighting();
                    this.UpdateAge();
                }
            }
        }

        public void ChangeWaterPark(string waterParkId)
        {
            Radical.EnsureComponent<Pickupable>(base.gameObject);
            bool flag = this.WaterParkCreature;
            if (flag)
            {
                this.WaterParkCreature.SetWaterPark(null);
            }
            this.CurrentWaterParkId = waterParkId;
            bool flag2 = this.CurrentWaterParkId.IsNotNull();
            if (flag2)
            {
                WaterPark waterPark = this.GetWaterPark(this.CurrentWaterParkId);
                bool flag3 = waterPark;
                if (flag3)
                {
                    this.WaterParkCreature.SetWaterPark(waterPark);
                    this.OnAddToWP();
                    this.WaterParkCreature.ValidatePosition();
                }
            }
            bool flag4 = this.AddedTime == 0.0;
            if (flag4)
            {
                this.AddedTime = this.GetAddedTime();
            }
        }

        public void UpdateLighting()
        {
            Int2 cell = this.WaterParkCreature.currentWaterPark.GetCell(this.WaterParkCreature);
            bool flag = cell != this.WaterParkCreature.currentWaterParkCell;
            if (flag)
            {
                this.WaterParkCreature.currentWaterParkCell = cell;
                this.WaterParkCreature.UpdateBaseLighting();
            }
        }

        public void UpdateAge()
        {
            bool flag = this.AddedTime > 0.0;
            if (flag)
            {
                this.WaterParkCreature.age = this.GetAge();
            }
            base.transform.localScale = Mathf.Lerp(this.WaterParkCreature.data.initialSize, this.WaterParkCreature.data.maxSize, this.WaterParkCreature.age) * Vector3.one;
        }

        public float GetAge()
        {
            double num = Network.Session.GetWorldTime() - this.AddedTime;
            return Mathf.InverseLerp(0f, this.WaterParkCreature.data.growingPeriod, (float)num);
        }

        public bool IsAdded()
        {
            return this.WaterParkCreature && this.WaterParkCreature.currentWaterPark;
        }

        public WaterPark GetWaterPark(string waterParkId)
        {
            BaseDeconstructable componentByGameObject = Network.Identifier.GetComponentByGameObject<BaseDeconstructable>(waterParkId, false);
            return (componentByGameObject != null) ? componentByGameObject.GetBaseWaterPark() : null;
        }

        public string GetWaterParkId()
        {
            return Network.DataStorage.GetProperty<string>(base.MultiplayerCreature.CreatureItem.Id.ToCreatureStringId(), "WaterParkId");
        }

        public double GetAddedTime()
        {
            return (double)Network.DataStorage.GetProperty<float>(base.MultiplayerCreature.CreatureItem.Id.ToCreatureStringId(), "AddedTime");
        }
    }
}
