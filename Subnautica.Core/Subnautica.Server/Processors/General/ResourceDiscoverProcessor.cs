namespace Subnautica.Server.Processors.General
{
    using Server.Core;
    using Subnautica.Network.Models.Core;
    using Subnautica.Server.Abstracts.Processors;
    using System.Collections.Generic;
    using System.Linq;
    using ServerModel = Subnautica.Network.Models.Server;

    public class ResourceDiscoverProcessor : NormalProcessor
    {
        public override bool OnExecute(AuthorizationProfile profile, NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.ResourceDiscoverArgs>();
            if (packet == null)
            {
                return this.SendEmptyPacketErrorLog(networkPacket);
            }

            if (Server.Instance.Storages.World.AddDiscoveredResource(packet.TechType))
            {
                packet.MapRooms = this.GetMapRooms();

                profile.SendPacketToAllClient(packet);
            }

            return true;
        }

        private List<string> GetMapRooms()
        {
            var response = new List<string>();

            foreach (var item in Server.Instance.Storages.Construction.Storage.Constructions.Where(q => q.Value.TechType == TechType.BaseMapRoom))
            {
                response.Add(item.Value.UniqueId);
            }

            return response;
        }
    }
}
