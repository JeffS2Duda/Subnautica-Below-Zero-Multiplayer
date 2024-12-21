namespace Subnautica.Client.Synchronizations.Processors.Player
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using ServerModel = Subnautica.Network.Models.Server;

    public class FreezeProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.PlayerFreezeArgs>();
            if (packet == null)
            {
                return false;
            }

            var player = ZeroPlayer.GetPlayerById(packet.GetPacketOwnerId());
            if (player != null)
            {
                if (packet.IsFreeze)
                {
                    player.EnableFreeze();
                }
                else
                {
                    player.DisableFreeze();
                }
            }

            return true;
        }

        public static void OnPlayerFreezed(PlayerFreezedEventArgs ev)
        {
            FreezeProcessor.SendPacketToServer(true, ev.EndTime);
        }

        public static void OnPlayerUnfreezed()
        {
            FreezeProcessor.SendPacketToServer(false);
        }

        public static void SendPacketToServer(bool isFreeze, float endTime = -1f)
        {
            ServerModel.PlayerFreezeArgs request = new ServerModel.PlayerFreezeArgs()
            {
                IsFreeze = isFreeze,
                EndTime = endTime
            };

            NetworkClient.SendPacket(request);
        }
    }
}
