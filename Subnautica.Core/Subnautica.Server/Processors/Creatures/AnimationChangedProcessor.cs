namespace Subnautica.Server.Processors.Creatures
{
    using Server.Core;
    
    using Subnautica.Network.Models.Core;
    using Subnautica.Server.Abstracts.Processors;

    using ServerModel = Subnautica.Network.Models.Server;

    public class AnimationChangedProcessor : NormalProcessor
    {
        public override bool OnExecute(AuthorizationProfile profile, NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.CreatureAnimationArgs>();
            if (packet == null)
            {
                return this.SendEmptyPacketErrorLog(networkPacket);
            }

            Server.Instance.Logices.CreatureWatcher.OnAnimationDataReceived(profile.PlayerId, packet.Animations);
            return true;
        }
    }
}
