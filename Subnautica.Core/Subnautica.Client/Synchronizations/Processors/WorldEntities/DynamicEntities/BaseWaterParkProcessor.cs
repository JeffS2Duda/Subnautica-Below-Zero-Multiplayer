namespace Subnautica.Client.Synchronizations.Processors.WorldEntities.DynamicEntities
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Extensions;
    using Subnautica.Network.Core.Components;

    using UnityEngine;

    using WorldEntityModel = Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;

    public class BaseWaterParkProcessor : WorldDynamicEntityProcessor
    {
        public override bool OnWorldLoadItemSpawn(NetworkDynamicEntityComponent packet, bool isDeployed, Pickupable pickupable, GameObject gameObject)
        {
            if (!isDeployed)
                return false;
            WorldEntityModel.WaterParkCreature component = packet.GetComponent<WorldEntityModel.WaterParkCreature>();
            if (component == null)
                return false;
            pickupable.MultiplayerDrop(waterParkId: component.WaterParkId, waterParkAddTime: component.AddedTime);
            return true;
        }
    }
}
