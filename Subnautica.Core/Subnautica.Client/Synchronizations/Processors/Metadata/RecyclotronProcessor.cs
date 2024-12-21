namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Server;

    using ServerModel = Subnautica.Network.Models.Server;
    using Metadata    = Subnautica.Network.Models.Metadata;

    public class RecyclotronProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(string uniqueId, TechType techType, MetadataComponentArgs packet, bool isSilence)
        {
            var component = packet.Component.GetComponent<Metadata.Recyclotron>();
            if (component == null)
            {
                return false;
            }

            if (component.IsRecycle)
            {
                var recyclotron = Network.Identifier.GetComponentByGameObject<global::Recyclotron>(uniqueId, true);
                if (recyclotron)
                {
                    recyclotron.recycleVFX.Play();
                }
            }

            return true;
        }

        public static void OnRecyclotronRecycle(RecyclotronRecycleEventArgs ev)
        {
            RecyclotronProcessor.SendPacketToServer(ev.UniqueId, isRecycle: true);
        }

        public static void SendPacketToServer(string uniqueId, bool isRecycle = false)
        {
            ServerModel.MetadataComponentArgs result = new ServerModel.MetadataComponentArgs()
            {
                UniqueId  = uniqueId,
                Component = new Metadata.Recyclotron()
                {
                    IsRecycle = isRecycle,
                },
            };

            NetworkClient.SendPacket(result);
        }
    }
}
