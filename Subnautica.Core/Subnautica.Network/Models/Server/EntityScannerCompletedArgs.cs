namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.WorldEntity;

    [MessagePackObject]
    public class EntityScannerCompletedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.EntityScannerCompleted;

        [Key(5)]
        public string ScannerPlayerUniqueId { get; set; }

        [Key(6)]
        public RestrictedEntity Entity { get; set; }
    }
}
