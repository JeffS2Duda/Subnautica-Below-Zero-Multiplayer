namespace Subnautica.Network.Models.Construction
{
    using MessagePack;

    using Subnautica.Network.Models.Construction.Shared;

    [MessagePackObject]
    public class BaseAddLadderGhostComponent : BaseGhostComponent
    {
        [Key(2)]
        public BaseFaceComponent FaceStart { get; set; }

        [Key(3)]
        public BaseFaceComponent FaceEnd { get; set; }
    }
}
