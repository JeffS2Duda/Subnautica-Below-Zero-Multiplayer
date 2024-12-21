namespace Subnautica.Client.Synchronizations.Processors.Inventory
{
    using Subnautica.API.Enums;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Network.Models.Core;
    using System.Collections;
    using UnityEngine;
    using UWE;
    using ServerModel = Subnautica.Network.Models.Server;


    public class EquipmentProcessor : NormalProcessor
    {
        private static bool IsSending { get; set; } = false;

        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            return true;
        }

        public static void OnProcessEquipment()
        {
            if (!IsSending && !EventBlocker.IsEventBlocked(ProcessType.InventoryEquipment))
            {
                UWE.CoroutineHost.StartCoroutine(SendServerData());
            }
        }

        private static IEnumerator SendServerData()
        {
            IsSending = true;

            yield return new WaitForSecondsRealtime(1f);

            IsSending = false;

            ServerModel.InventoryEquipmentArgs result = new ServerModel.InventoryEquipmentArgs()
            {
                Equipments = GetEquipments(),
                EquipmentSlots = global::Inventory.main.equipment.SaveEquipment()
            };

            NetworkClient.SendPacket(result);
        }


        private static byte[] GetEquipments()
        {
            using (PooledObject<ProtobufSerializer> serializer = ProtobufSerializerPool.GetProxy())
            {
                return StorageHelper.Save(serializer, global::Inventory.main.equipmentRoot);
            }
        }
    }
}