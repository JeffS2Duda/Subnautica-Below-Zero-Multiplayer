namespace Subnautica.Network.Models.Construction
{
    using MessagePack;

    using Subnautica.Network.Models.Construction.Shared;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class BaseAddCorridorGhostComponent : BaseGhostComponent
    {
        [Key(2)]
        public ZeroInt3 TargetOffset { get; set; }
    }
}
