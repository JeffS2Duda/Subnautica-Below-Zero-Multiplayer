namespace Subnautica.Server.Processors.PDA
{
    using Server.Core;

    using Subnautica.Network.Models.Core;
    using Subnautica.Server.Abstracts.Processors;

    using ServerModel = Subnautica.Network.Models.Server;

    public class TechAnalyzeAddedProcessor : NormalProcessor
    {
        public override bool OnExecute(AuthorizationProfile profile, NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.TechAnalyzeAddedArgs>();
            if (packet == null)
            {
                return this.SendEmptyPacketErrorLog(networkPacket);
            }

            if (Server.Instance.Storages.Technology.AddAnalyzedTechnology(packet.TechType))
            {
                profile.SendPacketToOtherClients(packet);
            }

            return true;
        }
    }
}
