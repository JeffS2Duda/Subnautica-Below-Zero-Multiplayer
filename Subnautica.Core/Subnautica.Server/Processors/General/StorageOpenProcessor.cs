namespace Subnautica.Server.Processors.General
{
    using Server.Core;

    using Subnautica.Network.Models.Core;
    using Subnautica.Server.Abstracts.Processors;

    using ServerModel = Subnautica.Network.Models.Server;

    public class StorageOpenProcessor : NormalProcessor
    {
        public override bool OnExecute(AuthorizationProfile profile, NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.StorageOpenArgs>();
            if (packet == null)
            {
                return this.SendEmptyPacketErrorLog(networkPacket);
            }

            if (Server.Instance.Logices.Interact.IsBlocked(packet.UniqueId))
            {
                return false;
            }


            Server.Instance.Logices.Interact.AddBlock(profile.UniqueId, packet.UniqueId, true);

            profile.SendPacketToAllClient(packet);
            return true;
        }
    }
}
