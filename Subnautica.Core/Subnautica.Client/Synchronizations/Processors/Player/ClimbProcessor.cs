namespace Subnautica.Client.Synchronizations.Processors.Player
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Client.Extensions;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using ServerModel = Subnautica.Network.Models.Server;

    public class ClimbProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.PlayerClimbArgs>();
            if (string.IsNullOrEmpty(packet.UniqueId))
            {
                return false;
            }

            var player = ZeroPlayer.GetPlayerById(packet.GetPacketOwnerId());
            if (player != null && player.IsMine)
            {
                player.OnHandClickClimb(packet.UniqueId);
            }
            else
            {
                player.ClimbStartCinematic(packet.UniqueId);
            }

            return true;
        }

        public static void OnPlayerClimbing(PlayerClimbingEventArgs ev)
        {
            ev.IsAllowed = false;

            if (!Network.HandTarget.IsBlocked(Network.Identifier.GetClimbUniqueId(ev.UniqueId)))
            {
                ServerModel.PlayerClimbArgs request = new ServerModel.PlayerClimbArgs()
                {
                    UniqueId = ev.UniqueId,
                    Duration = ev.Duration
                };

                NetworkClient.SendPacket(request);
            }
        }
    }
}
