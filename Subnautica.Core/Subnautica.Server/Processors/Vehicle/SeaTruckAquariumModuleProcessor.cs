namespace Subnautica.Server.Processors.Vehicle
{
    using Server.Core;
    using Subnautica.Network.Models.Core;
    using Subnautica.Server.Abstracts.Processors;
    using System.Linq;
    using Metadata = Subnautica.Network.Models.Metadata;
    using ServerModel = Subnautica.Network.Models.Server;
    using WorldEntityModel = Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;

    public class SeaTruckAquariumModuleProcessor : NormalProcessor
    {
        public override bool OnExecute(AuthorizationProfile profile, NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.SeaTruckAquariumModuleArgs>();
            if (packet == null)
            {
                return this.SendEmptyPacketErrorLog(networkPacket);
            }

            if (Server.Instance.Logices.Interact.IsBlocked(packet.UniqueId, profile.UniqueId))
            {
                return false;
            }

            var storageContainer = this.GetStorageContainer(packet.UniqueId);
            if (storageContainer == null)
            {
                return false;
            }

            if (packet.IsAdded)
            {
                if (Server.Instance.Logices.Storage.TryPickupItem(packet.WorldPickupItem, storageContainer, profile.InventoryItems))
                {
                    profile.SendPacketToAllClient(packet);
                }
            }
            else
            {
                if (Server.Instance.Logices.Storage.TryPickupItem(packet.WorldPickupItem, profile.InventoryItems, storageContainer))
                {
                    profile.SendPacketToAllClient(packet);
                }
            }

            return true;
        }

        private Metadata.StorageContainer GetStorageContainer(string lockerId)
        {
            foreach (var item in Server.Instance.Storages.World.Storage.DynamicEntities.Where(q => q.TechType == TechType.SeaTruckAquariumModule))
            {
                var component = item.Component.GetComponent<WorldEntityModel.SeaTruckAquariumModule>();
                var locker = component.Lockers.Where(q => q.UniqueId == lockerId).FirstOrDefault();
                if (locker != null)
                {
                    return locker.StorageContainer;
                }
            }

            return null;
        }
    }
}
