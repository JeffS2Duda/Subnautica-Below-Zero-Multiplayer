namespace Subnautica.Network.Models.Construction
{
    using MessagePack;

    using Subnautica.Network.Models.Construction.Shared;

    [MessagePackObject]
    public class BaseAddPartitionGhostComponent : BaseGhostComponent
    {
        [Key(2)]
        public BaseFaceComponent FaceStart { get; set; }
    }
}
