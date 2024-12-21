namespace Subnautica.Server.Logic
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Creatures.Datas;
    using Subnautica.Network.Structures;
    using Subnautica.Server.Abstracts;
    using Subnautica.Server.Core;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class VoidLeviathan : BaseLogic
    {
        private StopwatchItem Timing { get; set; } = new StopwatchItem(2000f);

        private HashSet<ushort> SpawnedCreatures { get; set; } = new HashSet<ushort>();

        private Dictionary<byte, double> PlayerTimes { get; set; } = new Dictionary<byte, double>();

        private BaseCreatureData Data { get; set; }

        private int MaxSpawn { get; set; } = 3;

        private bool IsCreatureSpawned { get; set; }

        private VoidLeviathansSpawner Spawner
        {
            get
            {
                return VoidLeviathansSpawner.main;
            }
        }

        public override void OnStart()
        {
            this.Data = TechType.GhostLeviathan.GetCreatureData();
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
            if (this.Timing.IsFinished())
            {
                this.Timing.Restart();

                if (this.IsLoaded())
                {
                    this.RemoveVoidLeviathans();
                    this.SpawnVoidLeviathans();

                    if (this.IsCreatureSpawned)
                    {
                        this.IsCreatureSpawned = false;

                        Server.Instance.Logices.CreatureWatcher.ImmediatelyTrigger();
                    }
                }
            }
        }

        public void OnPlayerDisconnected(AuthorizationProfile player)
        {
            this.PlayerTimes[player.PlayerId] = 0;
        }

        private void RemoveVoidLeviathans()
        {
            foreach (var creatureId in this.SpawnedCreatures.ToList())
            {
                if (Server.Instance.Logices.CreatureWatcher.TryGetCreature(creatureId, out var creature))
                {
                    if (creature.IsBusy() || creature.IsExistsOwnership())
                    {
                        continue;
                    }

                    Server.Instance.Logices.CreatureWatcher.UnRegisterCreature(creatureId);

                    this.SpawnedCreatures.Remove(creatureId);
                }
            }
        }

        private void SpawnVoidLeviathans()
        {
            foreach (var player in Server.Instance.GetPlayers())
            {
                if (player.IsFullConnected)
                {
                    player.SetInVoidBiome(this.Spawner.IsVoidBiome(player.GetBiome()));

                    if (player.IsInVoidBiome)
                    {
                        if (this.PlayerTimes.TryGetValue(player.PlayerId, out var nextTime) == false || nextTime == 0)
                        {
                            this.PlayerTimes[player.PlayerId] = this.CalculateTimeNextSpawn(true);
                        }

                        if (Server.Instance.Logices.World.GetServerTime() >= this.PlayerTimes[player.PlayerId])
                        {
                            var creatureCount = this.GetNearestCreatureCount(player.Position);
                            if (creatureCount < this.MaxSpawn)
                            {
                                this.SpawnCreature(player);
                            }
                            else
                            {
                                this.PlayerTimes[player.PlayerId] = this.CalculateTimeNextSpawn();
                            }
                        }
                    }
                    else
                    {
                        this.PlayerTimes[player.PlayerId] = 0;
                    }
                }
            }
        }

        private void SpawnCreature(AuthorizationProfile player)
        {
            if (this.TryGetSpawnPosition(player.Position.ToVector3(), out var spawnPosition))
            {
                this.IsCreatureSpawned = true;
                this.PlayerTimes[player.PlayerId] = this.CalculateTimeNextSpawn();
                this.SpawnedCreatures.Add(Server.Instance.Logices.CreatureWatcher.RegisterCreature(TechType.GhostLeviathan, spawnPosition.ToZeroVector3(), new ZeroQuaternion()));
            }
        }

        private bool TryGetSpawnPosition(Vector3 playerPosition, out Vector3 spawnPosition)
        {
            spawnPosition = Vector3.zero;

            for (int i = 0; i < 10; i++)
            {
                spawnPosition = playerPosition + UnityEngine.Random.onUnitSphere * (this.Data.VisibilityDistance * 0.9f);

                if (spawnPosition.y < -100f && this.Spawner.IsVoidBiome(LargeWorld.main.GetBiome(spawnPosition)))
                {
                    return true;
                }
            }

            return false;
        }

        private double CalculateTimeNextSpawn(bool first = false)
        {
            var currentTime = Server.Instance.Logices.World.GetServerTime();
            return first ? currentTime + this.Spawner.timeBeforeFirstSpawn : currentTime + this.Spawner.spawnInterval;
        }

        private int GetNearestCreatureCount(ZeroVector3 playerPosition)
        {
            int count = 0;

            foreach (var creatureId in this.SpawnedCreatures)
            {
                if (Server.Instance.Logices.CreatureWatcher.TryGetCreature(creatureId, out var creature))
                {
                    if (creature.Position.Distance(playerPosition) < this.Data.VisibilityDistance * this.Data.VisibilityDistance)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private bool IsLoaded()
        {
            return this.Spawner;
        }
    }
}
