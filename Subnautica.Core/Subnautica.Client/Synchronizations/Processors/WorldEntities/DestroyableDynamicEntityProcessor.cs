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

    public class DestroyableDynamicEntityProcessor : WorldEntityProcessor
    {
        public override bool OnDataReceived(NetworkWorldEntityComponent packet, byte requesterId, bool isSpawning)
        {
            var entity = packet.GetComponent<EntityModel.DestroyableDynamicEntity>();
            if (entity.UniqueId.IsNull())
            {
                return false;
            }

            if (entity.IsWorldStreamer)
            {
                World.DestroyPickupItem(entity.PickupItem);
            }
            else
            {
                Network.DynamicEntity.Remove(entity.UniqueId);
            }

            return true;
        }

        public static void OnTakeDamaging(TakeDamagingEventArgs ev)
        {
            if (ev.IsDead && ev.IsDestroyable && !ev.IsStaticWorldEntity)
            {
                if (ev.UniqueId.IsWorldStreamer() && !ev.LiveMixin.GetComponent<global::SpawnOnKill>())
                {
                    ev.IsAllowed = false;

                    DestroyableDynamicEntityProcessor.SendPacketToServer(ev.UniqueId, true);
                }
                else if (ev.UniqueId.IsNotNull() && Network.DynamicEntity.HasEntity(ev.UniqueId) && ev.LiveMixin.TryGetComponent<global::Plantable>(out var plantable) && ev.LiveMixin.GetComponentInParent<global::Planter>() == null)
                {
                    ev.IsAllowed = false;

                    DestroyableDynamicEntityProcessor.SendPacketToServer(ev.UniqueId, false);
                }
                else if (ev.TechType.IsCreatureEgg())
                {
                    ev.IsAllowed = false;

                    DestroyableDynamicEntityProcessor.SendPacketToServer(ev.UniqueId, false);
                }
            }
        }

        private static void SendPacketToServer(string uniqueId, bool isWorldStreamer)
        {
            ServerModel.WorldEntityActionArgs request = new ServerModel.WorldEntityActionArgs()
            {
                Entity = new EntityModel.DestroyableDynamicEntity()
                {
                    UniqueId        = uniqueId,
                    IsWorldStreamer = isWorldStreamer,
                },
            };

            NetworkClient.SendPacket(request);
        }
    }
}
