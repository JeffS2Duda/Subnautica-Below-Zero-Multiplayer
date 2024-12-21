namespace Subnautica.Client.Synchronizations.Processors.Building
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using Constructing = Subnautica.Client.Multiplayer.Constructing;
    using ServerModel = Subnautica.Network.Models.Server;

    public class RemovedProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.ConstructionRemovedArgs>();
            if (packet.TechType == TechType.None || string.IsNullOrEmpty(packet.UniqueId))
            {
                return false;
            }

            if (Constructing.Builder.GetBuildingProgressType(packet.UniqueId) == BuildingProgressType.Constructing)
            {
                using (EventBlocker.Create(ProcessType.ConstructingRemoved))
                {
                    Constructing.Builder.Destroy(packet.UniqueId, callSound: true);

                    ConstructionSyncedProcessor.UpdateConstructionSync();
                }
            }

            return true;
        }

        public override void OnStart()
        {
            this.SetWaitingForNextFrame(true);
        }

        public static void OnConstructingRemoved(ConstructionRemovedEventArgs ev)
        {
            ServerModel.ConstructionRemovedArgs request = new ServerModel.ConstructionRemovedArgs()
            {
                UniqueId = ev.UniqueId,
                TechType = ev.TechType,
                Cell = ev.Cell?.ToZeroInt3()
            };

            NetworkClient.SendPacket(request);
        }
    }
}
