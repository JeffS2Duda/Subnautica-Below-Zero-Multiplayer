namespace Subnautica.API.Features.Creatures.MonoBehaviours.Shared
{
    using UnityEngine;

    public class MultiplayerWaterParkCreature : BaseMultiplayerCreature
    {
        private global::WaterParkCreature WaterParkCreature { get; set; }

        private bool IsRegisteredWaterPark { get; set; }

        public void Awake()
        {
            this.WaterParkCreature = this.GetComponent<global::WaterParkCreature>();
        }

        public void Start()
        {
            this.HideCreature();
        }

        public void OnEnable()
        {
            this.HideCreature();
        }

        public void FixedUpdate()
        {
            if (this.IsRegisteredWaterPark == false)
            {
                this.RegisterWaterPark();
            }
        }

        private void RegisterWaterPark()
        {
            this.IsRegisteredWaterPark = true;
        }

        private void HideCreature()
        {
            this.gameObject.transform.localScale = Vector3.zero;
        }
    }
}
