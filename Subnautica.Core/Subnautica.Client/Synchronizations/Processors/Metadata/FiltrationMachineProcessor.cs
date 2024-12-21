namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Server;
    using System.Linq;
    using Metadata = Subnautica.Network.Models.Metadata;
    using ServerModel = Subnautica.Network.Models.Server;

    public class FiltrationMachineProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(string uniqueId, TechType techType, MetadataComponentArgs packet, bool isSilence)
        {
            var component = packet.Component.GetComponent<Metadata.FiltrationMachine>();
            if (component == null)
            {
                return false;
            }

            var gameObject = Network.Identifier.GetComponentByGameObject<global::BaseFiltrationMachineGeometry>(uniqueId);
            if (gameObject == null)
            {
                return false;
            }

            var machine = gameObject.GetModule();
            if (machine == null)
            {
                return false;
            }

            this.MachineTimeSync(machine, component);

            if (isSilence)
            {
                foreach (var item in component.Items.Where(q => !string.IsNullOrEmpty(q.ItemId)))
                {
                    Entity.SpawnToQueue(item.TechType, item.ItemId, machine.storageContainer.container);
                }

                return true;
            }

            if (!string.IsNullOrEmpty(component.RemovingItemId))
            {
                Entity.RemoveToQueue(component.RemovingItemId);
            }
            else if (component.Item != null)
            {
                Entity.SpawnToQueue(component.Item.TechType, component.Item.ItemId, machine.storageContainer.container);
            }

            return true;
        }

        private void MachineTimeSync(global::FiltrationMachine machine, Metadata.FiltrationMachine component)
        {
            machine.timeRemainingWater = component.TimeRemainingWater;
            machine.timeRemainingSalt = component.TimeRemainingSalt;
        }

        public static void OnStorageItemAdding(StorageItemAddingEventArgs ev)
        {
            if (ev.TechType == TechType.BaseFiltrationMachine)
            {
                ev.IsAllowed = false;
            }
        }

        public static void OnStorageItemRemoving(StorageItemRemovingEventArgs ev)
        {
            if (ev.TechType == TechType.BaseFiltrationMachine)
            {
                FiltrationMachineProcessor.SendPacketToServer(ev.UniqueId, removingItemId: ev.ItemId);
            }
        }

        public static void SendPacketToServer(string uniqueId, string removingItemId = null)
        {
            ServerModel.MetadataComponentArgs result = new ServerModel.MetadataComponentArgs()
            {
                UniqueId = uniqueId,
                Component = new Metadata.FiltrationMachine()
                {
                    RemovingItemId = removingItemId,
                },
            };

            NetworkClient.SendPacket(result);
        }
    }
}
