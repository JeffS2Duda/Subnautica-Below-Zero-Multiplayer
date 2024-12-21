namespace Subnautica.Client.Synchronizations.Processors.Player
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using ServerModel = Subnautica.Network.Models.Server;

    public class SubrootToggleProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.SubrootToggleArgs>();
            if (packet.GetPacketOwnerId() == 0)
            {
                return false;
            }

            var player = ZeroPlayer.GetPlayerById(packet.GetPacketOwnerId());
            if (player != null)
            {
                if (packet.IsEntered)
                {
                    player.SetSubRootId(packet.SubrootId);
                    player.SetInteriorId(null);
                }
                else
                {
                    player.SetSubRootId(null);
                }
            }

            return true;
        }

        public static void OnPlayerBaseEntered(PlayerBaseEnteredEventArgs ev)
        {
            SendDataToServer(ev.UniqueId, true);
        }

        public static void OnPlayerBaseExited(PlayerBaseExitedEventArgs ev)
        {
            SendDataToServer(ev.UniqueId, false);
        }

        private static void SendDataToServer(string subrootId, bool isEntered = false)
        {
            ServerModel.SubrootToggleArgs request = new ServerModel.SubrootToggleArgs()
            {
                SubrootId = subrootId,
                IsEntered = isEntered,
            };

            NetworkClient.SendPacket(request);
        }
    }
}
