namespace Subnautica.Client.Synchronizations.Processors.Story
{
    using System.Collections.Generic;
    using System.Linq;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using ClientModel = Subnautica.Network.Models.Client;
    using ServerModel = Subnautica.Network.Models.Server;

    public class PlayerVisibilityProcessor : NormalProcessor
    {
        private static string CurrentCinematic { get; set; } = null;

        private Queue<string> VisibilityQueue { get; set; } = new Queue<string>();

        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ClientModel.StoryPlayerVisibilityArgs>();

            foreach (var visibility in packet.Visibility)
            {
                var player = ZeroPlayer.GetPlayerByUniqueId(visibility.Key);
                if (player != null)
                {
                    player.IsStoryCinematicModeActive = visibility.Value;
                }

                this.VisibilityQueue.Enqueue(visibility.Key);
            }

            return true;
        }

        public override void OnFixedUpdate()
        {
            if (ZeroPlayer.CurrentPlayer.IsStoryCinematicModeActive)
            {
                foreach (var player in ZeroPlayer.GetPlayers())
                {
                    player.Hide();
                }
            }
            else
            {
                while (this.VisibilityQueue.Count > 0)
                {
                    var player = ZeroPlayer.GetPlayerByUniqueId(this.VisibilityQueue.Dequeue());
                    if (player != null)
                    {
                        if (player.IsStoryCinematicModeActive)
                        {
                            player.Hide();
                        }
                        else
                        {
                            player.Show();
                        }
                    }
                }
            }
        }

        public static void OnStoryCinematicStarted(StoryCinematicStartedEventArgs ev)
        {
            if (Network.Story.StoryCinematics.Any(q => ev.CinematicName.Contains(q)))
            {
                CurrentCinematic = ev.CinematicName;

                PlayerVisibilityProcessor.SendPacketToServer(true);
            }
        }

        public static void OnStoryCinematicCompleted(StoryCinematicCompletedEventArgs ev)
        {
            if (CurrentCinematic.IsNotNull() && ev.CinematicName == CurrentCinematic)
            {
                CurrentCinematic = null;

                PlayerVisibilityProcessor.SendPacketToServer(false);
            }
        }

        private static void SendPacketToServer(bool isCinematicActive)
        {
            ServerModel.StoryPlayerVisibilityArgs result = new ServerModel.StoryPlayerVisibilityArgs()
            {
                IsCinematicActive = isCinematicActive,
            };

            NetworkClient.SendPacket(result);
        }

        public override void OnDispose()
        {
            CurrentCinematic = null;
        }
    }
}
