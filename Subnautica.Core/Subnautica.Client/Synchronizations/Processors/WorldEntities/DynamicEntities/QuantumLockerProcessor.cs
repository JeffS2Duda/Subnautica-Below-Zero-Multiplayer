namespace Subnautica.Client.Synchronizations.Processors.WorldEntities.DynamicEntities
{
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Network.Core.Components;

    using UnityEngine;

    public class QuantumLockerProcessor : WorldDynamicEntityProcessor
    {
        public override bool OnWorldLoadItemSpawn(NetworkDynamicEntityComponent packet, bool isDeployed, Pickupable pickupable, GameObject gameObject)
        {
            var quantumLocker = gameObject.GetComponent<global::QuantumLocker>();
            if (quantumLocker)
            {
                quantumLocker.storageContainer.SetContainer(QuantumLockerStorage.main.storageContainer.container);
                quantumLocker.loadedFromSaveGame = true;

                QuantumLockerStorage.Register(quantumLocker);
            }

            return true;
        }
    }
}
