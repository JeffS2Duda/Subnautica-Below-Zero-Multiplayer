namespace Subnautica.Client.Synchronizations.Processors.Items
{
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Network.Models.Core;
    using ServerModel = Subnautica.Network.Models.Server;

    public class PlayerItemActionProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.PlayerItemActionArgs>();
            if (packet == null)
            {
                return false;
            }

            return PlayerItemProcessor.ExecuteProcessor(packet.Item, packet.GetPacketOwnerId());
        }
    }
}
