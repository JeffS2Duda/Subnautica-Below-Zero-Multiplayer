namespace Subnautica.Client.Synchronizations.Processors.World
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using EntityModel = Subnautica.Network.Models.WorldEntity;
    using ServerModel = Subnautica.Network.Models.Server;

    public class EntityScannerProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.EntityScannerCompletedArgs>();

            if (packet.Entity.UniqueId.IsWorldStreamer())
            {
                Network.WorldStreamer.DisableSlot(packet.Entity.UniqueId.WorldStreamerToSlotId(), -1);
            }
            else
            {
                Network.StaticEntity.AddStaticEntity(packet.Entity);

                var player = ZeroPlayer.GetPlayerByUniqueId(packet.ScannerPlayerUniqueId);
                if (player != null && player.IsMine)
                {
                    return false;
                }

                var gameObject = Network.Identifier.GetGameObject(packet.Entity.UniqueId, true);
                if (gameObject == null)
                {
                    return false;
                }

                World.DestroyGameObject(gameObject);
            }

            return true;
        }

        public static void OnEntityScannerCompleted(EntityScannerCompletedEventArgs ev)
        {
            if (ev.TechType.IsFragment() && ev.TechType.IsDestroyAfterScan() && ev.UniqueId.IsNotNull())
            {
                ServerModel.EntityScannerCompletedArgs request = new ServerModel.EntityScannerCompletedArgs()
                {
                    Entity = new EntityModel.RestrictedEntity()
                    {
                        UniqueId = ev.UniqueId,
                    }
                };

                NetworkClient.SendPacket(request);
            }
        }
    }
}
