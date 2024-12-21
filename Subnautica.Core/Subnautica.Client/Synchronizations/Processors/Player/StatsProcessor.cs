namespace Subnautica.Client.Synchronizations.Processors.Player
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using ServerModel = Subnautica.Network.Models.Server;

    public class StatsProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            return true;
        }

        public static void OnPlayerStatsUpdated(PlayerStatsUpdatedEventArgs ev)
        {
            if (World.IsLoaded)
            {
                ServerModel.PlayerStatsArgs request = new ServerModel.PlayerStatsArgs()
                {
                    Health = ev.Health,
                    Food = ev.Food,
                    Water = ev.Water,
                };

                NetworkClient.SendPacket(request);
            }
        }
    }
}
