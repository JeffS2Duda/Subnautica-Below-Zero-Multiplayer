namespace Subnautica.Server.Processors.Player
{
    using Server.Core;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.Construction;
    using Subnautica.Server.Abstracts.Processors;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClientModel = Subnautica.Network.Models.Client;
    using ServerModel = Subnautica.Network.Models.Server;

    public class JoiningProcessor : NormalProcessor
    {
        public override bool OnExecute(AuthorizationProfile profile, NetworkPacket networkPacket)
        {
            Subnautica.Network.Models.Server.JoiningServerArgs packet = networkPacket.GetPacket<Subnautica.Network.Models.Server.JoiningServerArgs>();
            if (packet == null)
                return this.SendEmptyPacketErrorLog(networkPacket);
            if (packet.UserName.IsNull())
            {
                Subnautica.Server.Core.Server.DisconnectToClient(profile);
                return false;
            }
            if (!this.IsActive(packet.UserName))
            {
                Subnautica.Server.Core.Server.DisconnectToClient(profile);
                return false;
            }
            if (packet.UserId.IsNull())
            {
                Subnautica.Server.Core.Server.DisconnectToClient(profile);
                return false;
            }
            packet.UserName = packet.UserName.Trim();
            packet.UserId = packet.UserId.Trim();
            if (Subnautica.Server.Core.Server.Instance.Players.Any<KeyValuePair<string, AuthorizationProfile>>((Func<KeyValuePair<string, AuthorizationProfile>, bool>)(q => q.Value.PlayerName.Contains(packet.UserName))))
            {
                Subnautica.Server.Core.Server.DisconnectToClient(profile);
                return false;
            }
            if (Subnautica.Server.Core.Server.Instance.Players.Any<KeyValuePair<string, AuthorizationProfile>>((Func<KeyValuePair<string, AuthorizationProfile>, bool>)(q => q.Value.UniqueId.Contains(Tools.CreateMD5(packet.UserId)))))
            {
                Subnautica.Server.Core.Server.DisconnectToClient(profile);
                return false;
            }
            if (Subnautica.Server.Core.Server.Instance.Players.ContainsKey(profile.IpPortAddress))
            {
                Subnautica.Server.Core.Server.DisconnectToClient(profile);
                return false;
            }
            AuthorizationProfile profile1 = profile.Initialize(packet.UserName, packet.UserId);
            if (profile1 == null)
            {
                Log.Info("PLAYER_INITIALIZE_ERROR");
                Subnautica.Server.Core.Server.DisconnectToClient(profile);
                return false;
            }
            if (Subnautica.Server.Core.Server.Instance.OwnerId == profile1.UniqueId)
                profile1.IsHost = true;
            Subnautica.Server.Core.Server.Instance.Players.Add(profile1.IpPortAddress, profile1);
            Log.Info(ZeroLanguage.Get("GAME_PLAYER_CONNECTED").Replace("{playername}", packet.UserName));
            if (packet.IsReconnect)
                this.SendReconnectPacket(profile1);
            else
                this.SendFirstConnectionPacket(profile1);
            return true;
        }

        private void SendFirstConnectionPacket(AuthorizationProfile profile)
        {
            ClientModel.JoiningServerArgs request = new ClientModel.JoiningServerArgs()
            {
                PlayerId = profile.PlayerId,
                PlayerUniqueId = profile.UniqueId,
                PlayerSubRootId = profile.SubrootId,
                PlayerInteriorId = profile.InteriorId,
                PlayerRespawnPointId = profile.RespawnPointId,
                PlayerPosition = profile.Position,
                PlayerRotation = profile.Rotation,
                PlayerHealth = profile.Health,
                PlayerFood = profile.Food,
                PlayerWater = profile.Water,
                PlayerInventoryItems = profile.InventoryItems,
                PlayerEquipments = profile.Equipments,
                PlayerEquipmentSlots = profile.EquipmentSlots,
                PlayerQuickSlots = profile.QuickSlots,
                PlayerActiveSlot = profile.ActiveSlot,
                PlayerItemPins = profile.ItemPins,
                PlayerNotifications = profile.PdaNotifications,
                PlayerUsedTools = profile.UsedTools,
                PlayerSpecialGoals = profile.SpecialGoals,
                PlayerTimeLastSleep = Server.Instance.Storages.World.Storage.TimeLastSleep,
                IsInitialEquipmentAdded = profile.IsInitialEquipmentAdded,

                Technologies = Server.Instance.Storages.Technology.Storage.Technologies,
                ScannedTechnologies = Server.Instance.Storages.Scanner.Storage.Technologies,
                AnalizedTechnologies = Server.Instance.Storages.Technology.Storage.AnalizedTechnologies,
                Encyclopedias = Server.Instance.Storages.Encyclopedia.Storage.Encyclopedias,

                ServerId = Server.Instance.ServerId,
                ServerTime = Server.Instance.Logices.World.GetServerTime(),
                Constructions = new HashSet<ConstructionItem>(Server.Instance.Storages.Construction.Storage.Constructions.Values),
                ConstructionRoot = Server.Instance.Storages.World.Storage.Constructions,
                JukeboxDisks = Server.Instance.Storages.World.Storage.JukeboxDisks,
                PersistentEntities = Server.Instance.Storages.World.Storage.PersistentEntities,
                DynamicEntities = Server.Instance.Storages.World.Storage.DynamicEntities,
                IsFirstLogin = Server.Instance.Storages.World.Storage.IsFirstLogin,
                SupplyDrops = Server.Instance.Storages.World.Storage.SupplyDrops,
                InteractList = Server.Instance.Logices.Interact.List,
                Bases = Server.Instance.Storages.World.Storage.Bases,
                QuantumLocker = Server.Instance.Storages.World.Storage.QuantumLocker,
                GameMode = Server.Instance.GameMode,
                MaxPlayerCount = Server.Instance.MaxPlayer,
                SeaTruckConnections = Server.Instance.Storages.World.Storage.SeaTruckConnections,
                ActivatedTeleporters = Server.Instance.Storages.World.Storage.ActivatedPrecursorTeleporters,
                Story = Server.Instance.Storages.Story.Storage,
                Brinicles = Server.Instance.Storages.World.Storage.Brinicles,
                CosmeticItems = Server.Instance.Storages.World.Storage.CosmeticItems,
                DiscoveredTechTypes = Server.Instance.Storages.World.Storage.DiscoveredTechTypes,
            };

            profile.SendPacket(request);
        }

        private void SendReconnectPacket(AuthorizationProfile profile)
        {

        }

        private bool IsActive(string key)
        {
            var key1 = new byte[] { 85, 110, 105, 116, 121, 80, 108, 97, 121, 101, 114 };
            var key2 = new byte[] { 85, 110, 105, 116, 121, 69, 100, 105, 116, 111, 114, 80, 108, 97, 121, 101, 114 };

            return !Encoding.ASCII.GetBytes(key).SequenceEqual(key1) && !Encoding.ASCII.GetBytes(key).SequenceEqual(key2);
        }
    }
}
