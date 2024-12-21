namespace Subnautica.Client.Synchronizations.Processors.Vehicle
{
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using ServerModel = Subnautica.Network.Models.Server;

    public class ExosuitJumpProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            return true;
        }

        public static void OnExosuitJumping(ExosuitJumpingEventArgs ev)
        {
            ServerModel.ExosuitJumpArgs request = new ServerModel.ExosuitJumpArgs()
            {
                UniqueId = ev.UniqueId,
            };

            NetworkClient.SendPacket(request);
        }
    }
}
