namespace Subnautica.Client.Synchronizations.Processors.Inventory
{
    using Subnautica.API.Enums;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Network.Models.Core;
    using System.Collections;
    using UnityEngine;
    using ServerModel = Subnautica.Network.Models.Server;

    public class ItemPinProcessor : NormalProcessor
    {
        private static bool IsSending { get; set; } = false;

        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            return true;
        }

        public static void OnProcessPin()
        {
            if (!IsSending && !EventBlocker.IsEventBlocked(ProcessType.ItemPin))
            {
                UWE.CoroutineHost.StartCoroutine(SendServerData());
            }
        }

        private static IEnumerator SendServerData()
        {
            IsSending = true;

            yield return new WaitForSecondsRealtime(0.5f);

            ServerModel.ItemPinArgs result = new ServerModel.ItemPinArgs()
            {
                Items = PinManager.main.pins
            };

            NetworkClient.SendPacket(result);

            IsSending = false;
        }
    }
}

