namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class WaterParkCreature : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public double AddedTime { get; set; }

        [Key(1)]
        public string WaterParkId { get; set; }

        public WaterParkCreature()
        {

        }

        public WaterParkCreature(double addedTime, string waterParkId)
        {
            this.AddedTime = addedTime;
            this.WaterParkId = waterParkId;
        }

        public void SpawnChildren(double currentTime)
        {
            this.AddedTime = currentTime;
        }

        public bool IsSpawnable(double currentTime)
        {
            return currentTime - this.AddedTime >= 1200;
        }
    }
}
