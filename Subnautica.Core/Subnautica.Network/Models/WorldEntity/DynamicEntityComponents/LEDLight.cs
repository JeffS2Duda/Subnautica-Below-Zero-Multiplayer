namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class LEDLight : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public bool IsDeployed { get; set; }
    }
}
