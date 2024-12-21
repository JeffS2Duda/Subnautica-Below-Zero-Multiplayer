using System;
using System.Collections.Generic;
namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class VehicleUpgradeConsoleArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.VehicleUpgradeConsole;

        [Key(5)]
        public string UniqueId { get; set; }

        [Key(6)]
        public string ItemId { get; set; }

        [Key(7)]
        public string SlotId { get; set; }

        [Key(8)]
        public bool IsOpening { get; set; }

        [Key(9)]
        public bool IsAdding { get; set; }

        [Key(10)]
        public TechType ModuleType { get; set; }
    }
}
