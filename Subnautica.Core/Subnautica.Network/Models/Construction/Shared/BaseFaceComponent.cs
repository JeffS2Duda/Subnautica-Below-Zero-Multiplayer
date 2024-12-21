namespace Subnautica.Network.Models.Construction.Shared
{
    using MessagePack;

    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class BaseFaceComponent
    {
        [Key(0)]
        public ZeroInt3 Cell { get; set; }

        [Key(1)]
        public Base.Direction Direction { get; set; }
    }
}
