namespace Subnautica.Client.Synchronizations.Processors.WorldEntities
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Core.Components;
    using System.Collections;
    using UnityEngine;
    using EntityModel = Subnautica.Network.Models.WorldEntity;
    using ServerModel = Subnautica.Network.Models.Server;

    public class DataboxProcessor : WorldEntityProcessor
    {
        public override bool OnDataReceived(NetworkWorldEntityComponent packet, byte requesterId, bool isSpawning)
        {
            var entity = packet.GetComponent<EntityModel.Databox>();
            if (string.IsNullOrEmpty(entity.UniqueId))
            {
                return false;
            }

            Network.StaticEntity.AddStaticEntity(entity);

            var databox = Network.Identifier.GetComponentByGameObject<global::BlueprintHandTarget>(entity.UniqueId, true);
            if (databox != null)
            {
                databox.used = true;

                if (isSpawning)
                {
                    UWE.CoroutineHost.StartCoroutine(this.PickupBlueprintAsync(databox));
                }
                else
                {
                    this.PickupBlueprint(databox);
                }
            }

            return true;
        }

        private IEnumerator PickupBlueprintAsync(global::BlueprintHandTarget databox)
        {
            yield return new WaitForSecondsRealtime(0.25f);

            if (databox != null)
            {
                this.PickupBlueprint(databox);
            }
        }

        private void PickupBlueprint(global::BlueprintHandTarget databox)
        {
            if (databox.animParam.IsNotNull())
            {
                databox.animator.SetBool(databox.animParam, true);
            }

            databox.used = true;
            databox.OnTargetUsed();
        }

        public static void OnDataboxItemPickedUp(DataboxItemPickedUpEventArgs ev)
        {
            ServerModel.WorldEntityActionArgs result = new ServerModel.WorldEntityActionArgs()
            {
                Entity = new EntityModel.Databox()
                {
                    UniqueId = ev.UniqueId,
                    IsUsed = true,
                }
            };

            NetworkClient.SendPacket(result);
        }
    }
}
