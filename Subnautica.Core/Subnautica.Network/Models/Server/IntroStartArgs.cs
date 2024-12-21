namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    using WorldChildrens = Subnautica.Network.Models.Storage.World.Childrens;

    [MessagePackObject]
    public class IntroStartArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.FirstTimeStartServer;

        [Key(5)]
        public WorldChildrens.SupplyDrop SupplyDrop { get; set; }

        [Key(6)]
        public bool IsFinished { get; set; }

        [Key(7)]
        public float ServerTime { get; set; }
    }
}
