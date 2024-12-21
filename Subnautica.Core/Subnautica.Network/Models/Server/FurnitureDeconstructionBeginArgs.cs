namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    using System.Collections.Generic;

    [MessagePackObject]
    public class FurnitureDeconstructionBeginArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.FurnitureDeconstructionBegin;
        
        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.Construction;

        [Key(3)]
        public override byte ChannelId { get; set; } = 1;

        [Key(5)]
        public uint Id { get; set; }

        [Key(6)]
        public string UniqueId { get; set; }
    }
}
