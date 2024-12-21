namespace Subnautica.Server.Logic.Furnitures
{
    using Subnautica.API.Features;
    using Subnautica.Network.Models.Metadata;
    using Subnautica.Server.Abstracts;
    using Subnautica.Server.Core;
    using System.Collections.Generic;
    using System.Linq;
    using Metadata = Subnautica.Network.Models.Metadata;
    using ServerModel = Subnautica.Network.Models.Server;
    using WorldEntityModel = Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;

    public class Bed : BaseLogic
    {
        public StopwatchItem Timing { get; set; } = new StopwatchItem(1000f);

        private float SleepGameTimeDuration { get; set; } = 396f;

        private float SleepRealTimeDuration { get; set; } = 5f;

        public override void OnStart()
        {
            foreach (var bed in this.GetBeds())
            {
                if (bed.IsUsing())
                {
                    bed.Standup();
                }
            }
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
            if (this.Timing.IsFinished() && World.IsLoaded)
            {
                this.Timing.Restart();

                if (this.IsTimeLastSleepAvailable() && !Server.Instance.Storages.World.Storage.SkipTimeMode)
                {
                    var sleepingPlayerCount = this.GetBeds().Count(q => q.IsSleeping(Server.Instance.Logices.World.GetServerTime()));
                    if (sleepingPlayerCount > 0 && sleepingPlayerCount >= Server.Instance.Players.Count && this.SkipTime(this.SleepGameTimeDuration, this.SleepRealTimeDuration))
                    {
                        Server.Instance.Storages.World.Storage.TimeLastSleep = Server.Instance.Logices.World.GetServerTime();

                        this.SendPacketToAllClient();
                    }
                }
            }
        }

        public void ClearPlayerBeds(byte playerId)
        {
            foreach (var bed in this.GetBeds())
            {
                if (bed.PlayerId_v2 == playerId)
                {
                    bed.Standup();
                }
            }
        }

        private void SendPacketToAllClient()
        {
            ServerModel.SleepTimeSkipArgs request = new ServerModel.SleepTimeSkipArgs()
            {
                TimeLastSleep = Server.Instance.Storages.World.Storage.TimeLastSleep,
                SkipModeEndTime = Server.Instance.Storages.World.Storage.SkipModeEndTime,
                TimeAmount = this.SleepGameTimeDuration,
                SkipDuration = this.SleepRealTimeDuration,
            };

            Core.Server.SendPacketToAllClient(request);
        }

        public bool IsTimeLastSleepAvailable()
        {
            return Server.Instance.Storages.World.Storage.TimeLastSleep + 600f <= Server.Instance.Logices.World.GetServerTime();
        }

        private bool SkipTime(float timeAmount, float skipDuration)
        {
            if (Server.Instance.Storages.World.Storage.SkipTimeMode || timeAmount <= 0.0 || skipDuration <= 0.0)
            {
                return false;
            }

            Server.Instance.Storages.World.Storage.SkipTimeMode = true;
            Server.Instance.Storages.World.Storage.SkipModeEndTime = Server.Instance.Logices.World.GetServerTime() + timeAmount;
            Server.Instance.Storages.World.Storage.WorldSpeed = timeAmount / skipDuration;
            return true;
        }

        public List<BedSideItem> GetBeds()
        {
            var beds = new List<BedSideItem>();

            foreach (var item in Server.Instance.Storages.Construction.Storage.Constructions.Where(q => q.Value.ConstructedAmount == 1f && API.Features.TechGroup.Beds.Contains(q.Value.TechType)))
            {
                beds.AddRange(item.Value.EnsureComponent<Metadata.Bed>().Sides);
            }

            foreach (var item in Server.Instance.Storages.World.Storage.DynamicEntities.Where(q => q.TechType == TechType.SeaTruckSleeperModule))
            {
                beds.Add(item.Component.GetComponent<WorldEntityModel.SeaTruckSleeperModule>().Bed);
            }

            return beds;
        }
    }
}
