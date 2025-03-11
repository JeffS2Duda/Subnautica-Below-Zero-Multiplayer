namespace Subnautica.Client.Synchronizations.Processors.General
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using System.Collections.Generic;
    using System.Linq;
    using ServerModel = Subnautica.Network.Models.Server;

    public class StorageOpenProcessor : NormalProcessor
    {

        private HashSet<string> OpenedStorages = new HashSet<string>();

        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.StorageOpenArgs>();
            if (packet == null)
            {
                return true;
            }

            this.OpenStorage(packet.UniqueId, packet.TechType, ZeroPlayer.IsPlayerMine(packet.GetPacketOwnerId()));
            return true;
        }

        public override void OnFixedUpdate()
        {
            if (World.IsLoaded && this.OpenedStorages.Count > 0)
            {
                foreach (var uniqueId in this.OpenedStorages.ToList())
                {
                    if (Interact.IsBlocked(uniqueId))
                    {
                        continue;
                    }

                    this.OpenedStorages.Remove(uniqueId);

                    this.CloseStorage(uniqueId);
                }
            }
        }

        public bool OpenStorage(string uniqueId, TechType techType, bool isMine = false)
        {
            using (EventBlocker.Create(TechType.BaseBioReactor))
            {
                using (EventBlocker.Create(TechType.BaseNuclearReactor))
                {
                    using (EventBlocker.Create(TechType.BaseWaterPark))
                    {
                        switch (techType)
                        {
                            case TechType.BaseWaterPark:
                                if (isMine)
                                {
                                    BaseDeconstructable componentByGameObject = Subnautica.API.Features.Network.Identifier.GetComponentByGameObject<BaseDeconstructable>(uniqueId);
                                    WaterPark baseWaterPark = componentByGameObject != null ? componentByGameObject.GetBaseWaterPark() : null;
                                    if (baseWaterPark)
                                    {
                                        IHandTarget componentInChildren = (IHandTarget)baseWaterPark.GetComponentInChildren<LargeRoomWaterParkPlanter>();
                                        if (componentInChildren != null)
                                            componentInChildren.OnHandClick((GUIHand)null);
                                        else
                                            baseWaterPark.planter.storageContainer.Open(baseWaterPark.transform);
                                    }
                                    break;
                                }
                                break;
                            case TechType.BaseMapRoom:
                                if (isMine)
                                {
                                    BaseDeconstructable componentByGameObject = Subnautica.API.Features.Network.Identifier.GetComponentByGameObject<BaseDeconstructable>(uniqueId);
                                    MapRoomFunctionality roomFunctionality = componentByGameObject != null ? componentByGameObject.GetMapRoomFunctionality() : null;
                                    if (roomFunctionality)
                                        roomFunctionality.storageContainer.Open(roomFunctionality.transform);
                                    break;
                                }
                                break;
                            case TechType.BaseBioReactor:
                                if (isMine)
                                {
                                    Network.Identifier.GetComponentByGameObject<BaseBioReactorGeometry>(uniqueId, true)?.OnUse(null);
                                    break;
                                }
                                break;
                            case TechType.BaseNuclearReactor:
                                if (isMine)
                                {
                                    Network.Identifier.GetComponentByGameObject<BaseNuclearReactorGeometry>(uniqueId, true)?.OnUse(null);
                                    break;
                                }
                                break;
                            default:
                                StorageContainer componentByGameObject1 = Network.Identifier.GetComponentByGameObject<StorageContainer>(uniqueId, true);
                                if (componentByGameObject1)
                                {
                                    if (isMine)
                                    {
                                        componentByGameObject1.Open(componentByGameObject1.transform);
                                    }
                                    else
                                    {
                                        switch (techType)
                                        {
                                            case TechType.EscapePod:
                                            case TechType.SmallStorage:
                                            case TechType.QuantumLocker:
                                            case TechType.Recyclotron:
                                            case TechType.Exosuit:
                                                componentByGameObject1.open = true;
                                                this.OpenedStorages.Add(uniqueId);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            return true;
        }

        public void CloseStorage(string uniqueId)
        {
            var gameObject = Network.Identifier.GetComponentByGameObject<global::StorageContainer>(uniqueId, true);
            if (gameObject)
            {
                gameObject.open = false;
            }
        }

        public static void OnStorageOpening(StorageOpeningEventArgs ev)
        {
            ev.IsAllowed = false;

            if (!Interact.IsBlocked(ev.ConstructionId))
            {
                StorageOpenProcessor.SendPacketToServer(ev.ConstructionId, ev.TechType);
            }
        }

        public static void SendPacketToServer(string uniqueId, TechType techType)
        {
            ServerModel.StorageOpenArgs request = new ServerModel.StorageOpenArgs()
            {
                UniqueId = uniqueId,
                TechType = techType,
            };

            NetworkClient.SendPacket(request);
        }
    }
}