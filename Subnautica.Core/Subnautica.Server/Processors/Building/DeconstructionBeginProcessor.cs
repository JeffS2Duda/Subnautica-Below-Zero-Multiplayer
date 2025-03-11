namespace Subnautica.Server.Processors.Building
{
    using Server.Core;

    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Server;
    using Subnautica.Network.Models.Storage.Construction;
    using Subnautica.Server.Abstracts.Processors;

    public class DeconstructionBeginProcessor : NormalProcessor
    {
        public override bool OnExecute(AuthorizationProfile profile, NetworkPacket networkPacket)
        {
            DeconstructionBeginArgs packet = networkPacket.GetPacket<DeconstructionBeginArgs>();
            if (packet == null)
                return this.SendEmptyPacketErrorLog(networkPacket);
            if (Server.Instance.Logices.Interact.IsBlocked(packet.UniqueId))
                return false;
            ConstructionItem construction = Server.Instance.Storages.Construction.GetConstruction(packet.UniqueId);
            if (construction == null)
                return false;
            packet.Id = construction.Id;
            if (Server.Instance.Storages.Construction.IsDeconstructable(packet.UniqueId))
            {
                Server.Instance.Logices.Interact.AddBlock(profile.UniqueId, packet.UniqueId, true);
                Server.Instance.Logices.Interact.RemoveBlockByPlayerId(profile.UniqueId, 0.65f);
                if (construction.TechType == TechType.BaseWaterPark)
                    Server.Instance.Logices.BaseWaterPark.OnDeconstructionBegin(packet);
                else if (Server.Instance.Storages.Construction.UpdateConstructionAmount(packet.UniqueId, 0.98f))
                    profile.SendPacketToAllClient((NetworkPacket)packet);
            }
            else
            {
                packet.IsFailed = true;
                profile.SendPacketToAllClient((NetworkPacket)packet);
            }
            return true;
        }
    }
}
