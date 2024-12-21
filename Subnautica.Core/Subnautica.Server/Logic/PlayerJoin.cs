using Oculus.Platform;

namespace Subnautica.Server.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    using Subnautica.API.Features;
    using Subnautica.Network.Models.WorldStreamer;
    using Subnautica.Server.Abstracts;
    using Subnautica.Server.Core;

    using ClientModel = Subnautica.Network.Models.Client;

    public class PlayerJoin : BaseLogic
    {
        public List<KeyValuePair<string, int>> Queue { get; set; } = new List<KeyValuePair<string, int>>();

        public override void OnUnscaledFixedUpdate(float fixedDeltaTime)
        {
            if (Server.Instance.Logices.WorldStreamer.IsGeneratedWorld())
            {
                foreach (var item in this.Queue)
                {
                    var player = Server.Instance.GetPlayer(item.Key);
                    if (player == null)
                    {
                        continue;
                    }

                    if (item.Value == Server.Instance.Logices.WorldStreamer.GetSpawnPointCount())
                    {
                        this.SendJoinPacket(player, true);
                    }
                    else
                    {
                        this.SendJoinPacket(player, false);
                    }
                }

                this.Queue.Clear();
            }
        }

        public void OnPlayerDisconnected(string uniqueId)
        {
            this.Queue.RemoveAll(q => q.Key == uniqueId);
        }

        public void AddQueue(string uniqueId, int spawnPointCount)
        {
            this.Queue.Add(new KeyValuePair<string, int>(uniqueId, spawnPointCount));
        }

        private void SendJoinPacket(AuthorizationProfile player, bool isSpawnPointExists)
        {
            ClientModel.WorldLoadedArgs request = new ClientModel.WorldLoadedArgs()
            {
                IsSpawnPointRequest = true,
                IsSpawnPointExists  = isSpawnPointExists,
                SpawnPoints         = this.GetSpawnPoints(isSpawnPointExists),
            };

            player.SendPacket(request);
        }

        private HashSet<ZeroSpawnPointSimple> GetSpawnPoints(bool isSpawnPointExists)
        {
            if (isSpawnPointExists)
            {
                return Server.Instance.Storages.World.Storage.SpawnPoints.Where(q => q.NextRespawnTime != 0 || q.Health != -1f).ToHashSet();
            }

            return Server.Instance.Storages.World.Storage.SpawnPoints;
        }
    }
}

