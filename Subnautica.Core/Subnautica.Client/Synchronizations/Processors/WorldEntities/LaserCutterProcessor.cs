namespace Subnautica.Client.Synchronizations.Processors.WorldEntities
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Core.Components;

    using EntityModel = Subnautica.Network.Models.WorldEntity;
    using ServerModel = Subnautica.Network.Models.Server;

    public class LaserCutterProcessor : WorldEntityProcessor
    {
        public override bool OnDataReceived(NetworkWorldEntityComponent packet, byte requesterId, bool isSpawning)
        {
            var entity = packet.GetComponent<EntityModel.SealedObject>();
            if (entity.UniqueId.IsNull())
            {
                return false;
            }

            Network.StaticEntity.AddStaticEntity(entity);

            var sealedObject = Network.Identifier.GetComponentByGameObject<global::Sealed>(entity.UniqueId, true);
            if (sealedObject)
            {
                sealedObject.openedAmount = entity.Amount;

                if (!entity.IsSealed)
                {
                    sealedObject.openedEvent.Trigger(sealedObject);
                    sealedObject._sealed = false;
                }
            }

            return true;
        }

        public static void OnSealedInitialized(SealedInitializedEventArgs ev)
        {
            var sealedObject = Network.StaticEntity.GetEntity<EntityModel.SealedObject>(ev.UniqueId);
            if (sealedObject != null)
            {
                ev.SealedObject.openedAmount = sealedObject.Amount;
                ev.SealedObject._sealed      = sealedObject.IsSealed;
            }
        }

        public static void OnLaserCutterUsing(LaserCutterEventArgs ev)
        {
            ev.IsAllowed = false;
            
            ServerModel.WorldEntityActionArgs result = new ServerModel.WorldEntityActionArgs()
            {
                Entity = new EntityModel.SealedObject()
                {
                    UniqueId  = ev.UniqueId,
                    Amount    = ev.Amount,
                    MaxAmount = ev.MaxAmount,
                }
            };

            NetworkClient.SendPacket(result);
        }
    }
}
