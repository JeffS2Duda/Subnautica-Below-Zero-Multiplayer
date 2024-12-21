namespace Subnautica.Client.Synchronizations.Processors.PDA
{
    using Subnautica.API.Enums;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using ServerModel = Subnautica.Network.Models.Server;

    public class LogAddedProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.PDALogAddedArgs>();

            using (EventBlocker.Create(ProcessType.PDALogAdded))
            {
            }

            return true;
        }

        public static void OnPDALogAdded(PDALogAddedEventArgs ev)
        {
        }
    }
}