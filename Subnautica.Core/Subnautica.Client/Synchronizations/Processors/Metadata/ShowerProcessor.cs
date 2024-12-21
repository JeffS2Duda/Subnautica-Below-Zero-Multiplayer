namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Server;
    using Metadata = Subnautica.Network.Models.Metadata;
    using ServerModel = Subnautica.Network.Models.Server;

    public class ShowerProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(string uniqueId, TechType techType, MetadataComponentArgs packet, bool isSilence)
        {
            if (isSilence)
            {
                return false;
            }

            var component = packet.Component.GetComponent<Metadata.Shower>();
            if (component == null)
            {
                return false;
            }

            var gameObject = Network.Identifier.GetComponentByGameObject<global::ToggleOnClick>(uniqueId);
            if (gameObject == null)
            {
                return false;
            }

            if (component.IsActive)
            {
                ZeroGame.ToggleClickSwitchOn(gameObject);
            }
            else
            {
                ZeroGame.ToggleClickSwitchOff(gameObject);
            }

            return true;
        }

        public static void OnShowerSwitchToggle(ShowerSwitchToggleEventArgs ev)
        {
            ev.IsAllowed = false;

            ServerModel.MetadataComponentArgs result = new ServerModel.MetadataComponentArgs()
            {
                UniqueId = ev.UniqueId,
                Component = new Metadata.Shower(ev.SwitchStatus),
            };

            NetworkClient.SendPacket(result);
        }
    }
}
