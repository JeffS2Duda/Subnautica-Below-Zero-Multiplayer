namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Core;
    using Subnautica.Client.MonoBehaviours.Entity;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Server;
    using System.Linq;
    using UnityEngine;
    using Metadata = Subnautica.Network.Models.Metadata;
    using ServerModel = Subnautica.Network.Models.Server;

    public class CrafterProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(string uniqueId, TechType techType, MetadataComponentArgs packet, bool isSilence)
        {
            var component = packet.Component.GetComponent<Metadata.Crafter>();
            if (component == null)
            {
                return false;
            }

            if (!isSilence && component.CrafterClone != null)
            {
                Network.Session.SetConstructionComponent(uniqueId, component.CrafterClone);
            }

            var gameObject = Network.Identifier.GetGameObject(packet.UniqueId, true);
            if (gameObject == null)
            {
                return false;
            }

            var crafter = gameObject.EnsureComponent<MultiplayerCrafter>();
            if (crafter == null)
            {
                return false;
            }

            crafter.Initialize();
            crafter.SetIsMine(ZeroPlayer.IsPlayerMine(packet.GetPacketOwnerId()));

            if (component.IsOpened)
            {
                crafter.Open();
            }

            if (component.CraftingTechType != TechType.None)
            {
                if (component.CrafterClone != null)
                {
                    crafter.Craft(component.CrafterClone.CraftingTechType, component.CrafterClone.CraftingStartTime, component.CrafterClone.CraftingDuration);
                }
                else
                {
                    crafter.Craft(component.CraftingTechType, component.CraftingStartTime, component.CraftingDuration);
                }
            }
            else if (component.IsPickup)
            {
                crafter.TryPickup();
            }
            else if (!component.IsOpened)
            {
                crafter.Close();
            }

            return true;
        }

        public static void OnEntitySpawned(EntitySpawnedEventArgs ev)
        {
            if (ev.TechType == TechType.Fabricator)
            {
                var fabricator = Network.Session.Current.Constructions.FirstOrDefault(q => q.IsStatic && q.UniqueId == ev.UniqueId);
                if (fabricator?.Component != null)
                {
                    MetadataProcessor.ExecuteProcessor(TechType.Fabricator, fabricator.UniqueId, fabricator.Component, true);
                }
            }
        }

        public static void OnCrafterItemPickup(CrafterItemPickupEventArgs ev)
        {
            ev.IsAllowed = false;

            if (Interact.IsBlocked(ev.UniqueId))
            {
                return;
            }

            var crafter = GetMultiplayerGameObject(ev.Crafter).EnsureComponent<MultiplayerCrafter>();
            if (crafter.IsAllowedPickup(ev.TechType, ev.Amount))
            {

                CrafterProcessor.SendDataToServer(ev.UniqueId, isPickup: true);
            }
            else
            {
                ErrorMessage.AddMessage(global::Language.main.Get("InventoryFull"));
            }
        }

        public static void OnCrafterBegin(CrafterBeginEventArgs ev)
        {
            ev.IsAllowed = false;

            CrafterProcessor.SendDataToServer(ev.UniqueId, techType: ev.TechType, duration: ev.Duration);
        }

        public static void OnCrafterOpening(CrafterOpeningEventArgs ev)
        {
            ev.IsAllowed = false;

            if (!Interact.IsBlocked(ev.UniqueId))
            {
                CrafterProcessor.SendDataToServer(ev.UniqueId, isOpening: true);
            }
        }

        public static void OnCrafterClosed(CrafterClosedEventArgs ev)
        {
            if (ev.FabricatorType != TechType.Constructor)
            {
                CrafterProcessor.SendDataToServer(ev.UniqueId);
            }
        }

        public static void OnCrafterEnded(CrafterEndedEventArgs ev)
        {
            if (World.IsLoaded)
            {
                var crafter = GetMultiplayerGameObject(ev.Crafter).EnsureComponent<MultiplayerCrafter>();
                if (crafter)
                {
                    if (crafter.IsActiveAutoPickup())
                    {
                        CrafterProcessor.SendDataToServer(ev.UniqueId, isPickup: true);
                    }
                    else
                    {
                        crafter.OnCrafterEnded();
                    }
                }
            }
        }

        public static GameObject GetMultiplayerGameObject(global::GhostCrafter crafter)
        {
            if (crafter.name.Contains("SeaTruckFabricator"))
            {
                return crafter.GetComponentInParent<AddressablesPrefabSpawn>().gameObject;
            }

            return crafter.gameObject;
        }

        private static void SendDataToServer(string uniqueId, bool isOpening = false, bool isPickup = false, TechType techType = TechType.None, float duration = 0.0f)
        {
            ServerModel.MetadataComponentArgs result = new ServerModel.MetadataComponentArgs()
            {
                UniqueId = uniqueId,
                SecretTechType = TechType.Fabricator,
                Component = new Metadata.Crafter()
                {
                    IsOpened = isOpening,
                    IsPickup = isPickup,
                    CraftingDuration = duration,
                    CraftingTechType = techType,
                },
            };

            NetworkClient.SendPacket(result);
        }
    }
}
