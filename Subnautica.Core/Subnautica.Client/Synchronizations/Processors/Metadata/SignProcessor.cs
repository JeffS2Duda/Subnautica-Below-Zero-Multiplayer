namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Server;
    using Metadata = Subnautica.Network.Models.Metadata;
    using ServerModel = Subnautica.Network.Models.Server;

    public class SignProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(string uniqueId, TechType techType, MetadataComponentArgs packet, bool isSilence)
        {
            var component = packet.Component.GetComponent<Metadata.Sign>();
            if (component == null)
            {
                return false;
            }

            var gameObject = Network.Identifier.GetComponentByGameObject<global::uGUI_SignInput>(uniqueId);
            if (gameObject == null)
            {
                return false;
            }

            using (EventBlocker.Create(TechType.Sign))
            {
                if (gameObject.colorIndex != component.ColorIndex)
                {
                    gameObject.colorIndex = component.ColorIndex;
                }

                if (gameObject.scaleIndex != component.ScaleIndex)
                {
                    gameObject.scaleIndex = component.ScaleIndex;
                }

                gameObject.text = component.Text;
                gameObject.elementsState = component.ElementsState;

                if (gameObject.backgroundToggle != null)
                {
                    gameObject.backgroundToggle.isOn = component.IsBackgroundEnabled;
                }
            }

            return true;
        }

        public static void OnSignSelect(SignSelectEventArgs ev)
        {
            if (ev.TechType == TechType.SmallLocker || ev.TechType == TechType.Locker || ev.TechType == TechType.Sign)
            {
                if (Interact.IsBlocked(ev.UniqueId))
                {
                    ev.IsAllowed = false;
                }
                else
                {
                    SignProcessor.SendDataToServer(ev.UniqueId, true);
                }
            }
        }

        public static void OnSignDataChanged(SignDataChangedEventArgs ev)
        {
            if (ev.TechType == TechType.SmallLocker || ev.TechType == TechType.Locker || ev.TechType == TechType.Sign)
            {
                SignProcessor.SendDataToServer(ev.UniqueId, false, true, ev.Text, ev.ElementsState, ev.ScaleIndex, ev.ColorIndex, ev.IsBackgroundEnabled);
            }
        }

        public static void SendDataToServer(string uniqueId, bool isOpening = false, bool isSave = false, string text = null, bool[] elementsState = null, int scaleIndex = 0, int colorIndex = 0, bool isBackgroundEnabled = false)
        {
            ServerModel.MetadataComponentArgs result = new ServerModel.MetadataComponentArgs()
            {
                UniqueId = uniqueId,
                SecretTechType = TechType.Sign,
                Component = new Metadata.Sign()
                {
                    Text = text,
                    ScaleIndex = scaleIndex,
                    ColorIndex = colorIndex,
                    ElementsState = elementsState,
                    IsBackgroundEnabled = isBackgroundEnabled,
                    IsOpening = isOpening,
                    IsSave = isSave,
                },
            };

            NetworkClient.SendPacket(result);
        }
    }
}
