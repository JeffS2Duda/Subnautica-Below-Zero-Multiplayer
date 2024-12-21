namespace Subnautica.Network.Models.Construction
{
    using MessagePack;

    using Subnautica.Network.Models.Construction.Shared;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class BaseAddConnectorGhostComponent : BaseGhostComponent
    {
        [Key(2)]
        public ZeroInt3 FaceCell { get; set; }
    }
}
