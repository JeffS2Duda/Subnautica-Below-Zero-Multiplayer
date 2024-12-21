namespace Subnautica.Network.Models.Construction
{
    using MessagePack;

    using Subnautica.Network.Models.Construction.Shared;

    [MessagePackObject]
    public class BaseAddFaceGhostComponent : BaseGhostComponent
    {
        [Key(2)]
        public BaseFaceComponent Face { get; set; }
    }
}
