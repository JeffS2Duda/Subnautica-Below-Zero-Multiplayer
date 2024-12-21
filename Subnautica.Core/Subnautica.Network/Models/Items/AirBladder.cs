namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class AirBladder : NetworkPlayerItemComponent
    {
        [Key(2)]
        public float Value { get; set; }
    }
}
