namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using System.Linq;

    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Helper;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Client.Extensions;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Server;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;

    using UnityEngine;

    using Metadata         = Subnautica.Network.Models.Metadata;
    using ServerModel      = Subnautica.Network.Models.Server;
    using WorldEntityModel = Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;

    public class BaseWaterParkProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(string uniqueId, TechType techType, MetadataComponentArgs packet, bool isSilence)
        {
            return true;
        }

        public void OnEntitySpawned(ItemQueueProcess item, Pickupable pickupable, GameObject gameObject)
        {
            var uniqueId = item.Action.GetProperty<string>("CustomProperty");
            var entity   = item.Action.GetProperty<WorldDynamicEntity>("Entity");
            if (entity != null)
            {
                var component = entity.Component.GetComponent<WorldEntityModel.WaterParkCreature>();
                if (component != null)
                {
                    pickupable.MultiplayerDrop(true, waterParkId: uniqueId, waterParkAddTime: component.AddedTime);
                }
            }
        }

        public static void OnPlayerItemDroping(PlayerItemDropingEventArgs ev)
        {
            if (ev.Item.GetTechType().IsCreatureEgg())
            {
                var waterParkId = ZeroPlayer.CurrentPlayer.GetCurrentWaterParkUniqueId();
                if (waterParkId.IsNotNull())
                {
                    ev.IsAllowed = false;

                }
            }
        }

    }
}
