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

    public class DestroyableEntityProcessor : WorldEntityProcessor
    {
        public override bool OnDataReceived(NetworkWorldEntityComponent packet, byte requesterId, bool isSpawning)
        {
            var entity = packet.GetComponent<EntityModel.DestroyableEntity>();
            if (entity.UniqueId.IsNull())
            {
                return false;
            }

            Network.StaticEntity.AddStaticEntity(entity);

            var liveMixin = Network.Identifier.GetComponentByGameObject<global::LiveMixin>(entity.UniqueId, true);
            if (liveMixin == null)
            {
                return false;
            }

            if (entity.IsSpawnable)
            {
                liveMixin.health = entity.Health;
            }
            else
            {
                liveMixin.Kill();
            }

            return true;
        }

        public static void OnTakeDamaging(TakeDamagingEventArgs ev)
        {
            if (ev.IsStaticWorldEntity && ev.IsDestroyable && ev.Damage > 0f)
            {
                ServerModel.WorldEntityActionArgs request = new ServerModel.WorldEntityActionArgs()
                {
                    Entity = new EntityModel.DestroyableEntity()
                    {
                        UniqueId = ev.UniqueId,
                        Health = ev.NewHealth,
                    },
                };

                NetworkClient.SendPacket(request);
            }
        }
    }
}
