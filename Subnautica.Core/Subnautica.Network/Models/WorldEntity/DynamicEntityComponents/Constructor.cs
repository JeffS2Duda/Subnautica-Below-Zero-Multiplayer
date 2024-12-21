namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Constructor : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public bool IsDeployed { get; set; } = true;

        [Key(1)]
        public float CraftingFinishTime { get; set; }

        public bool IsCraftable(float currentTime)
        {
            return currentTime >= this.CraftingFinishTime;
        }
    }
}
