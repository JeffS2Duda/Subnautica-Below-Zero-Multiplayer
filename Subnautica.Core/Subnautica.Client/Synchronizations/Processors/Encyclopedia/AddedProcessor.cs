    namespace Subnautica.Client.Synchronizations.Processors.Encyclopedia
{
    using Subnautica.Client.Abstracts;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using Subnautica.Client.Core;
    using Subnautica.API.Features;
    using Subnautica.API.Enums;

    using ServerModel = Subnautica.Network.Models.Server;

    public class AddedProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.EncyclopediaAddedArgs>();

            using (EventBlocker.Create(ProcessType.EncyclopediaAdded))
            {
                PDAEncyclopedia.Add(packet.Key, packet.Verbose, false);

                NotificationManager.main.Add(NotificationManager.Group.Encyclopedia, packet.Key, 0f);
            }

            return true;
        }

        public static void OnEncyclopediaAdded(EncyclopediaAddedEventArgs ev)
        {
            ServerModel.EncyclopediaAddedArgs result = new ServerModel.EncyclopediaAddedArgs()
            {
                Key     = ev.Key,
                Verbose = ev.Verbose,
            };

            NetworkClient.SendPacket(result);
        }
    }
}