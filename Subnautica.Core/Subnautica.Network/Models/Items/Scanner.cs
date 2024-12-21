namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Scanner : NetworkPlayerItemComponent
    {
        [Key(1)]
        public override TechType TechType { get; set; } = TechType.Scanner;

        [Key(4)]
        public string TargetId { get; set; }
    }
}
