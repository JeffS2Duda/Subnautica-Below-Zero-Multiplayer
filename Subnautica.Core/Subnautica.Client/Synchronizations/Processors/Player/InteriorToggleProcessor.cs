namespace Subnautica.Client.Synchronizations.Processors.Player
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using ServerModel = Subnautica.Network.Models.Server;

    public class InteriorToggleProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.InteriorToggleArgs>();
            if (packet.GetPacketOwnerId() == 0)
            {
                return false;
            }

            var player = ZeroPlayer.GetPlayerById(packet.GetPacketOwnerId());
            if (player != null)
            {
                if (packet.IsEntered)
                {
                    player.SetInteriorId(packet.InteriorId);
                }
                else
                {
                    player.SetInteriorId(null);
                }
            }

            return true;
        }

        public static void OnEnteredInterior(PlayerEnteredInteriorEventArgs ev)
        {
            InteriorToggleProcessor.SendPacketToServer(ev.UniqueId, true);
        }

        public static void OnExitedInterior(PlayerExitedInteriorEventArgs ev)
        {
            InteriorToggleProcessor.SendPacketToServer(null, false);
        }

        private static void SendPacketToServer(string interiorId, bool isEntered = false)
        {
            ServerModel.InteriorToggleArgs request = new ServerModel.InteriorToggleArgs()
            {
                InteriorId = interiorId,
                IsEntered = isEntered,
            };

            NetworkClient.SendPacket(request);
        }
    }
}
