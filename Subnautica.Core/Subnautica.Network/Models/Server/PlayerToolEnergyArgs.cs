namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Metadata;

    [MessagePackObject]
    public class PlayerToolEnergyArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.PlayerToolEnergy;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public StorageItem Item { get; set; }
    }
}
