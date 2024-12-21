namespace Subnautica.Client.Synchronizations.Processors.Building
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using ServerModel = Subnautica.Network.Models.Server;

    public class DeconstructionBeginProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.DeconstructionBeginArgs>();
            if (packet.UniqueId.IsNull())
            {
                return false;
            }

            if (packet.IsFailed)
            {
                ErrorMessage.AddMessage(global::Language.main.Get("DeconstructAttachedError"));
            }
            else
            {
                using (EventBlocker.Create(ProcessType.DeconstructionBegin))
                {
                    Multiplayer.Constructing.Builder.Deconstruct(packet.UniqueId, packet.Id);

                    ConstructionSyncedProcessor.UpdateConstructionSync();
                }
            }

            return true;
        }

        public override void OnStart()
        {
            this.SetWaitingForNextFrame(true);
        }

        public static void OnDeconstructionBegin(DeconstructionBeginEventArgs ev)
        {
            ev.IsAllowed = false;

            if (!Network.HandTarget.IsBlocked(ev.UniqueId))
            {
                ServerModel.DeconstructionBeginArgs request = new ServerModel.DeconstructionBeginArgs()
                {
                    UniqueId = ev.UniqueId,
                };

                NetworkClient.SendPacket(request);
            }
        }
    }
}