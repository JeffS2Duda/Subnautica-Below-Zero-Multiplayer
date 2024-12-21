namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Metadata;
    using Subnautica.Network.Models.Server;
    using System.Collections.Generic;
    using UnityEngine;
    using Metadata = Subnautica.Network.Models.Metadata;
    using ServerModel = Subnautica.Network.Models.Server;

    public class BaseControlRoomProcessor : MetadataProcessor
    {
        private Dictionary<string, BaseControlRoomMap> ControlRooms { get; set; } = new Dictionary<string, BaseControlRoomMap>();

        private List<string> RemovingMaps { get; set; } = new List<string>();

        public override bool OnDataReceived(string uniqueId, TechType techType, MetadataComponentArgs packet, bool isSilence)
        {
            var component = packet.Component.GetComponent<Metadata.BaseControlRoom>();
            if (component == null)
            {
                return false;
            }

            var gameObject = Network.Identifier.GetComponentByGameObject<global::BaseControlRoom>(uniqueId);
            if (gameObject == null)
            {
                return false;
            }

            if (component.IsNavigateOpening)
            {
                using (EventBlocker.Create(TechType.BaseControlRoom))
                {
                    gameObject.OhClickMinimapConsole(null);
                }
            }
            else if (component.IsColorCustomizerSave)
            {
                var customizeable = gameObject.GetComponentInParent<ICustomizeable>();
                if (customizeable != null)
                {
                    customizeable.SetName(component.Name);
                    customizeable.SetColor(0, uGUI_ColorPicker.HSBFromColor(component.BaseColor.ToColor()), component.BaseColor.ToColor());
                    customizeable.SetColor(1, uGUI_ColorPicker.HSBFromColor(component.StripeColor1.ToColor()), component.StripeColor1.ToColor());
                    customizeable.SetColor(2, uGUI_ColorPicker.HSBFromColor(component.StripeColor2.ToColor()), component.StripeColor2.ToColor());
                    customizeable.SetColor(3, uGUI_ColorPicker.HSBFromColor(component.NameColor.ToColor()), component.NameColor.ToColor());
                }
            }
            else if (component.Minimap != null)
            {
                if (component.Minimap.Cell != null)
                {
                    var baseComp = gameObject.GetComponentInParent<global::Base>();
                    if (baseComp)
                    {
                        baseComp.SetPowered(component.Minimap.Cell.ToInt3(), component.Minimap.IsPowered);
                        gameObject.mapDirty = true;
                    }
                }
                else if (component.Minimap.Position != null)
                {
                    if (!this.ControlRooms.ContainsKey(packet.UniqueId))
                    {
                        this.ControlRooms.Add(packet.UniqueId, new BaseControlRoomMap());
                    }

                    if (this.ControlRooms[packet.UniqueId].Minimap == null)
                    {
                        this.ControlRooms[packet.UniqueId].Minimap = gameObject.minimapBase;
                    }

                    this.ControlRooms[packet.UniqueId].Position = component.Minimap.Position.ToVector3();
                    this.ControlRooms[packet.UniqueId].Time = Time.time;
                }
            }

            return true;
        }

        public override void OnUpdate()
        {
            if (this.ControlRooms.Count <= 0)
            {
                return;
            }

            foreach (var controlRoom in this.ControlRooms)
            {
                if (controlRoom.Value.Minimap == null || Time.time - controlRoom.Value.Time >= 0.2f)
                {
                    this.RemovingMaps.Add(controlRoom.Key);
                }
                else
                {
                    controlRoom.Value.Minimap.transform.localPosition = Vector3.MoveTowards(controlRoom.Value.Minimap.transform.localPosition, controlRoom.Value.Position, Time.deltaTime);
                }
            }

            if (this.RemovingMaps.Count > 0)
            {
                foreach (var item in this.RemovingMaps)
                {
                    this.ControlRooms.Remove(item);
                }

                this.RemovingMaps.Clear();
            }
        }

        public static void OnSubNameInputSelecting(SubNameInputSelectingEventArgs ev)
        {
            if (ev.TechType == TechType.BaseControlRoom)
            {
                if (!Interact.IsBlocked(TechGroup.GetBaseControlRoomCustomizerId(ev.UniqueId)))
                {
                    BaseControlRoomProcessor.SendPacketToServer(ev.UniqueId, isColorCustomizerOpening: true);
                }
                else if (Interact.IsBlocked(TechGroup.GetBaseControlRoomCustomizerId(ev.UniqueId), true))
                {
                    ev.IsAllowed = false;
                }
            }
        }

        public static void OnSubNameInputDeselected(SubNameInputDeselectedEventArgs ev)
        {
            if (ev.TechType == TechType.BaseControlRoom)
            {
                BaseControlRoomProcessor.SendPacketToServer(ev.UniqueId, ev.Name, ev.BaseColor, ev.StripeColor1, ev.StripeColor2, ev.NameColor, isColorCustomizerSave: true);
            }
        }

        public static void OnBaseControlRoomMinimapUsing(BaseControlRoomMinimapUsingEventArgs ev)
        {
            ev.IsAllowed = false;

            if (!Interact.IsBlocked(TechGroup.GetBaseControlRoomNavigateId(ev.UniqueId)))
            {
                BaseControlRoomProcessor.SendPacketToServer(ev.UniqueId, isNavigateOpening: true);
            }
        }

        public static void OnBaseControlRoomMinimapExiting(BaseControlRoomMinimapExitingEventArgs ev)
        {
            BaseControlRoomProcessor.SendPacketToServer(ev.UniqueId, minimap: new BaseControlRoomMinimap(ev.MapPosition.ToZeroVector3(), null), isNavigationExiting: true);
        }

        public static void OnBaseControlRoomCellPowerChanging(BaseControlRoomCellPowerChangingEventArgs ev)
        {
            ev.IsAllowed = false;

            BaseControlRoomProcessor.SendPacketToServer(ev.UniqueId, minimap: new BaseControlRoomMinimap(null, ev.Cell.ToZeroInt3()));
        }

        public static void OnBaseControlRoomMinimapMoving(BaseControlRoomMinimapMovingEventArgs ev)
        {
            BaseControlRoomProcessor.SendPacketToServer(ev.UniqueId, minimap: new BaseControlRoomMinimap(ev.Position.ToZeroVector3(), null));
        }

        public static void SendPacketToServer(string uniqueId, string name = null, Color baseColor = default(Color), Color stripeColor1 = default(Color), Color stripeColor2 = default(Color), Color nameColor = default(Color), BaseControlRoomMinimap minimap = null, bool isColorCustomizerOpening = false, bool isColorCustomizerSave = false, bool isNavigateOpening = false, bool isNavigationExiting = false)
        {
            ServerModel.MetadataComponentArgs result = new ServerModel.MetadataComponentArgs()
            {
                UniqueId = uniqueId,
                Component = new Metadata.BaseControlRoom()
                {
                    Name = name,
                    BaseColor = baseColor.ToZeroColor(),
                    StripeColor1 = stripeColor1.ToZeroColor(),
                    StripeColor2 = stripeColor2.ToZeroColor(),
                    NameColor = nameColor.ToZeroColor(),
                    Minimap = minimap,
                    IsColorCustomizerOpening = isColorCustomizerOpening,
                    IsColorCustomizerSave = isColorCustomizerSave,
                    IsNavigateOpening = isNavigateOpening,
                    IsNavigationExiting = isNavigationExiting,
                }
            };

            NetworkClient.SendPacket(result);
        }

        public override void OnDispose()
        {
            this.ControlRooms.Clear();
            this.RemovingMaps.Clear();
        }
    }

    public class BaseControlRoomMap
    {
        public GameObject Minimap { get; set; }

        public Vector3 Position { get; set; }

        public float Time { get; set; }
    }
}
