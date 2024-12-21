namespace Subnautica.Server.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    using Subnautica.API.Features;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Server.Abstracts;

    using Metadata         = Subnautica.Network.Models.Metadata;
    using ServerModel      = Subnautica.Network.Models.Server;
    using WorldEntityModel = Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;

    public class SeaTruckAquarium : BaseLogic
    {
        public StopwatchItem Timing { get; set; } = new StopwatchItem(5000f);
        public List<ServerModel.SeaTruckAquariumModuleArgs> Requests { get; set; } = new List<ServerModel.SeaTruckAquariumModuleArgs>();

        public override void OnFixedUpdate(float fixedTime)
        {
            if (this.Timing.IsFinished() && API.Features.World.IsLoaded)
            {
                this.Timing.Restart();

                var currentTime = Core.Server.Instance.Logices.World.GetServerTime();

                foreach (var module in this.GetSeaTruckAquariumModules())
                {
                    var component = module.Component.GetComponent<WorldEntityModel.SeaTruckAquariumModule>();

                    if (currentTime > component.LeftStorageTime)
                    {
                        component.LeftStorageTime = currentTime + Tools.GetRandomInt(55, 80);

                        var pickupItem = this.GetNewPickupItem();

                        if (component.Lockers.ElementAt(0).StorageContainer.HasRoomFor(pickupItem.GetStorageItem()))
                        {
                            component.Lockers.ElementAt(0).StorageContainer.AddItem(pickupItem.GetStorageItem());

                            this.Requests.Add(new ServerModel.SeaTruckAquariumModuleArgs()
                            {
                                IsAdded         = true,
                                UniqueId        = component.Lockers.ElementAt(0).UniqueId,
                                WorldPickupItem = pickupItem,
                            });
                        }
                    }

                    if (currentTime > component.RightStorageTime)
                    {
                        component.RightStorageTime = currentTime + Tools.GetRandomInt(55, 80);

                        var pickupItem = this.GetNewPickupItem();

                        if (component.Lockers.ElementAt(1).StorageContainer.HasRoomFor(pickupItem.GetStorageItem()))
                        {
                            component.Lockers.ElementAt(1).StorageContainer.AddItem(pickupItem.GetStorageItem());

                            this.Requests.Add(new ServerModel.SeaTruckAquariumModuleArgs()
                            {
                                IsAdded         = true, 
                                UniqueId        = component.Lockers.ElementAt(1).UniqueId,
                                WorldPickupItem = pickupItem,
                            });
                        }
                    }
                }

                this.SendPacketToAllClients();
            }
        }

        private void SendPacketToAllClients()
        {
            if (this.Requests.Count > 0)
            {
                foreach (var packet in this.Requests)
                {
                    Core.Server.SendPacketToAllClient(packet);
                }

                this.Requests.Clear();
            }
        }

        private WorldPickupItem GetNewPickupItem()
        {
            return WorldPickupItem.Create(Metadata.StorageItem.Create(Network.Identifier.GenerateUniqueId(), this.GetFishType()), API.Enums.PickupSourceType.None);
        }

        private TechType GetFishType()
        {
            var randomIndex = Tools.GetRandomInt(0, 100);
            if (randomIndex < 70)
            {
                switch (Tools.GetRandomInt(0, 3))
                {
                    default:
                    case 0: return TechType.Bladderfish;
                    case 1: return TechType.Hoopfish;
                    case 2: return TechType.ArcticPeeper;
                    case 3: return TechType.Boomerang;
                }
            }
            else if (randomIndex < 80)
            {
                return TechType.DiscusFish;
            }
            else if (randomIndex < 90)
            {
                switch (Tools.GetRandomInt(0, 1))
                {
                    default:
                    case 0: return TechType.FeatherFish;
                    case 1: return TechType.FeatherFishRed;
                }
            }

            return TechType.SpinnerFish;
        }

        private List<WorldDynamicEntity> GetSeaTruckAquariumModules()
        {
            return Core.Server.Instance.Storages.World.Storage.DynamicEntities.Where(q => q.TechType == TechType.SeaTruckAquariumModule).ToList();
        }
    }
}
