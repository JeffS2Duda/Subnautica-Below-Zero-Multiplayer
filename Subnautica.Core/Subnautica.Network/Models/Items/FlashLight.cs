namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class FlashLight : NetworkPlayerItemComponent
    {
        [Key(2)]
        public bool IsActivated { get; set; }
    }
}
