namespace Subnautica.Server.Processors.Story
{
    using Server.Core;
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Models.Core;
    using Subnautica.Server.Abstracts.Processors;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ServerModel = Subnautica.Network.Models.Server;

    public class CinematicProcessor : NormalProcessor
    {
        private Dictionary<StoryCinematicType, double> CinematicTimes = new Dictionary<StoryCinematicType, double>();

        public override bool OnExecute(AuthorizationProfile profile, NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.StoryCinematicTriggerArgs>();
            if (packet == null || packet.UniqueId.IsNull())
            {
                return this.SendEmptyPacketErrorLog(networkPacket);
            }

            Log.Info("[DEBUG] CinematicProcessor -> IsClick: " + packet.IsTypeClick + ", UniqueId: " + packet.UniqueId + ", Goal: " + packet.CinematicType.ToString() + ", IsCompleteable: " + Server.Instance.Logices.StoryTrigger.IsCompleteableCinematic(packet.CinematicType.ToString()));

            if (packet.IsTypeClick)
            {
                if (!Server.Instance.Logices.StoryTrigger.IsCompleteableCinematic(packet.CinematicType.ToString()))
                {
                    return false;
                }

                if (packet.CinematicType == StoryCinematicType.StoryEndGameRepairPillar1 || packet.CinematicType == StoryCinematicType.StoryEndGameRepairPillar2)
                {
                    if (!this.IsPillarActivateable(packet.CinematicType))
                    {
                        return false;
                    }
                }
            }

            packet.StartTime = Server.Instance.Logices.World.GetServerTimeAsDouble() + 0.15;

            if (packet.CinematicType == StoryCinematicType.StoryEndGameEnterShip)
            {
                if (Server.Instance.Logices.StoryTrigger.IsCompleteableCinematic(packet.CinematicType.ToString()))
                {
                    Server.Instance.Logices.StoryTrigger.CompleteTrigger(packet.CinematicType.ToString());

                    profile.SendPacketToAllClient(packet);
                }
            }
            else
            {
                if (Server.Instance.Storages.Story.CompleteCinematic(packet.CinematicType))
                {
                    Server.Instance.Logices.StoryTrigger.CompleteTrigger(packet.CinematicType.ToString());

                    profile.SendPacketToAllClient(packet);
                }
            }

            if (packet.CinematicType == StoryCinematicType.StoryEndGameGoToHomeWorld)
            {
                Task.Delay(2500).ContinueWith(q => this.CloseServerAsync());
            }

            return true;
        }

        private void CloseServerAsync()
        {
            Server.SendPacketToAllClient(new ServerModel.StoryEndGameArgs());
        }

        private bool IsPillarActivateable(StoryCinematicType cinematicType)
        {
            if (!this.CinematicTimes.ContainsKey(StoryCinematicType.StoryEndGameRepairPillar1))
            {
                this.CinematicTimes.Add(StoryCinematicType.StoryEndGameRepairPillar1, 0f);
            }

            if (!this.CinematicTimes.ContainsKey(StoryCinematicType.StoryEndGameRepairPillar2))
            {
                this.CinematicTimes.Add(StoryCinematicType.StoryEndGameRepairPillar2, 0f);
            }

            if (cinematicType == StoryCinematicType.StoryEndGameRepairPillar1)
            {
                if (this.CinematicTimes[StoryCinematicType.StoryEndGameRepairPillar2] >= Server.Instance.Logices.World.GetServerTimeAsDouble())
                {
                    return false;
                }
            }
            else
            {
                if (this.CinematicTimes[StoryCinematicType.StoryEndGameRepairPillar1] >= Server.Instance.Logices.World.GetServerTimeAsDouble())
                {
                    return false;
                }
            }

            this.CinematicTimes[cinematicType] = Server.Instance.Logices.World.GetServerTimeAsDouble() + 17.25;
            return true;
        }
    }
}
