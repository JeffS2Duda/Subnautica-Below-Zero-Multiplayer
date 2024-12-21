namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Seaglide : NetworkPlayerItemComponent
    {
        [Key(2)]
        public bool IsActivated { get; set; }

        [Key(3)]
        public bool IsLightsActivated { get; set; }
    }
}
