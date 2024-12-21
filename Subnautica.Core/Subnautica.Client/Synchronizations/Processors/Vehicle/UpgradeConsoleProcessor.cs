namespace Subnautica.Client.Synchronizations.Processors.Vehicle
{
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using Subnautica.API.Features;

    using ServerModel = Subnautica.Network.Models.Server;

    public class UpgradeConsoleProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.VehicleUpgradeConsoleArgs>();
            if (string.IsNullOrEmpty(packet.UniqueId))
            {
                return false;
            }

            var vehicle = Network.Identifier.GetComponentByGameObject<global::VehicleUpgradeConsoleInput>(packet.UniqueId);
            if (vehicle == null)
            {
                return false;
            }

            if (packet.IsOpening)
            {
                vehicle.OpenPDA();
            }
            else
            {
                if (string.IsNullOrEmpty(packet.ItemId))
                {
                    return false;
                }

                if (packet.IsAdding)
                {
                    Entity.SpawnToQueue(packet.SlotId, packet.ModuleType, packet.ItemId, vehicle.equipment);
                }
                else
                {
                    Entity.RemoveToQueue(packet.ItemId);
                }
            }

            return true;
        }


        public static void OnUpgradeConsoleOpening(UpgradeConsoleOpeningEventArgs ev)
        {
            ev.IsAllowed = false;

            if (!Interact.IsBlocked(ev.UniqueId))
            {
                UpgradeConsoleProcessor.SendPacketToServer(ev.UniqueId, isOpening: true);
            } 
        }


        public static void OnUpgradeConsoleModuleAdded(UpgradeConsoleModuleAddedEventArgs ev)
        {
            UpgradeConsoleProcessor.SendPacketToServer(ev.UniqueId, itemId: ev.ItemId, slotId: ev.SlotId, moduleType: ev.ModuleType, isAdding: true); 
        }

        public static void OnUpgradeConsoleModuleRemoved(UpgradeConsoleModuleRemovedEventArgs ev)
        {
            UpgradeConsoleProcessor.SendPacketToServer(ev.UniqueId, itemId: ev.ItemId, slotId: ev.SlotId);
        }

        public static void SendPacketToServer(string uniqueId, string itemId = null, string slotId = null, TechType moduleType = TechType.None, bool isOpening = false, bool isAdding = false)
        {
            ServerModel.VehicleUpgradeConsoleArgs request = new ServerModel.VehicleUpgradeConsoleArgs()
            {
                UniqueId   = uniqueId,
                ItemId     = itemId,
                SlotId     = slotId,
                ModuleType = moduleType,
                IsOpening  = isOpening,
                IsAdding   = isAdding
            };

            NetworkClient.SendPacket(request);
        }
    }
}
