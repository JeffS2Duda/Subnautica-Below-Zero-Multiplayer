namespace Subnautica.Server.Core
{
    using LiteNetLib;
    using MessagePack;
    using Story;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Core;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Creatures;
    using Subnautica.Network.Models.Metadata;
    using Subnautica.Network.Models.Storage.Player;
    using Subnautica.Network.Models.Storage.Story.StoryGoals;
    using Subnautica.Network.Structures;
    using Subnautica.Server.Events;
    using Subnautica.Server.Events.EventArgs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ServerModel = Subnautica.Network.Models.Server;

    [MessagePackObject]
    public class AuthorizationProfile
    {
        [IgnoreMember]
        private string ipPortAddress;

        [IgnoreMember]
        public byte PlayerId { get; set; }

        [IgnoreMember]
        public string UniqueId { get; set; }

        [IgnoreMember]
        public string IpAddress { get; set; }

        [IgnoreMember]
        public string IpPortAddress
        {
            get
            {
                return this.ipPortAddress;
            }
            set
            {
                this.ipPortAddress = value;

                if (this.ipPortAddress.IsNotNull())
                {
                    this.IpAddress = this.ipPortAddress.Split(':')[0].Trim();
                }
                else
                {
                    this.IpAddress = null;
                }
            }
        }

        [IgnoreMember]
        public bool IsHost { get; set; } = false;

        [IgnoreMember]
        public bool IsAuthorized { get; set; } = false;

        [IgnoreMember]
        public bool IsFullConnected { get; set; } = false;

        [IgnoreMember]
        public NetPeer NetPeer { get; set; }

        [IgnoreMember]
        public string VehicleId { get; set; }

        [IgnoreMember]
        public string WeatherProfileId { get; set; }

        [IgnoreMember]
        public bool IsStoryCinematicModeActive { get; set; } = false;

        [IgnoreMember]
        public string UsingRoomId { get; set; }

        [IgnoreMember]
        public float LastAttackTime { get; set; }

        [IgnoreMember]
        public bool IsInVoidBiome { get; private set; }

        [Key(0)]
        public string PlayerName { get; set; }

        [Key(1)]
        public string SubrootId { get; set; }

        [Key(2)]
        public string InteriorId { get; set; }

        [Key(3)]
        public float Health { get; set; } = 100f;

        [Key(4)]
        public float Water { get; set; } = 90.5f;

        [Key(5)]
        public float Food { get; set; } = 50.5f;

        [Key(6)]
        public ZeroVector3 Position { get; set; } = new ZeroVector3();

        [Key(7)]
        public ZeroQuaternion Rotation { get; set; } = new ZeroQuaternion();

        [Key(8)]
        public StorageContainer InventoryItems { get; set; }

        [Key(9)]
        public byte[] Equipments { get; set; }

        [Key(10)]
        public Dictionary<string, string> EquipmentSlots { get; set; } = new Dictionary<string, string>();

        [Key(11)]
        public string[] QuickSlots { get; set; }

        [Key(12)]
        public int ActiveSlot { get; set; }

        [Key(13)]
        public List<TechType> ItemPins { get; set; } = new List<TechType>();

        [Key(14)]
        public HashSet<NotificationItem> PdaNotifications { get; set; } = new HashSet<NotificationItem>();

        [Key(15)]
        public HashSet<TechType> UsedTools { get; set; } = new HashSet<TechType>();

        [Key(16)]
        public HashSet<ZeroStoryGoal> SpecialGoals { get; set; } = new HashSet<ZeroStoryGoal>();

        [Key(17)]
        public string RespawnPointId { get; set; }

        [Key(18)]
        public bool IsInitialEquipmentAdded { get; set; }

        [Key(19)]
        public float LastHypnotizeTime { get; set; }

        public AuthorizationProfile()
        {
        }

        public AuthorizationProfile(NetPeer netPeer)
        {
            this.IpPortAddress = netPeer.ToString();
            this.NetPeer = netPeer;
            this.PlayerId = Server.Instance.GetNextPlayerId();
        }

        public AuthorizationProfile Initialize(string playerName, string uniqueId)
        {
            uniqueId = Tools.CreateMD5(uniqueId);
            if (uniqueId.IsNull())
            {
                return null;
            }

            var profile = Server.Instance.Storages.Player.GetPlayerData(uniqueId, playerName);
            if (profile == null)
            {
                Log.Error("PLAYER DATA ERROR");
                return null;
            }

            profile.IsAuthorized = true;
            profile.IpPortAddress = this.IpPortAddress;
            profile.NetPeer = this.NetPeer;
            profile.PlayerName = playerName;
            profile.UniqueId = uniqueId;
            profile.PlayerId = this.PlayerId;

            if (profile.InventoryItems == null)
            {
                profile.InventoryItems = StorageContainer.Create(6, 8);
            }

            return profile;
        }

        public void OnFullConnected()
        {
            this.IsFullConnected = true;

            try
            {
                PlayerFullConnectedEventArgs args = new PlayerFullConnectedEventArgs(this);

                Handlers.OnPlayerFullConnected(args);
            }
            catch (Exception e)
            {
                Log.Error($"AuthorizationProfile.OnFullConnected: {e}\n{e.StackTrace}");
            }
        }

        public void OnDisconnected()
        {
            this.IsFullConnected = false;

            try
            {
                PlayerDisconnectedEventArgs args = new PlayerDisconnectedEventArgs(this);

                Handlers.OnPlayerDisconnected(args);
            }
            catch (Exception e)
            {
                Log.Error($"AuthorizationProfile.OnDisconnected: {e}\n{e.StackTrace}");
            }

            Log.Info("DISCONNECT PLAYER -> " + this.PlayerName);

            Server.Instance.Logices.CreatureWatcher.OnPlayerDisconnected(this.PlayerId);
            Server.Instance.Logices.Bed.ClearPlayerBeds(this.PlayerId);
            Server.Instance.Logices.EntityWatcher.RemoveOwnershipByPlayer(this.UniqueId);
            Server.Instance.Logices.Hoverpad.RemovePlayerFromPlatform(this.UniqueId, true);
            Server.Instance.Logices.Interact.RemoveBlockByPlayerId(this.UniqueId);
            Server.Instance.Logices.PlayerJoin.OnPlayerDisconnected(this.UniqueId);
            Server.Instance.Logices.BaseMapRoom.OnPlayerDisconnected(this.UniqueId);
            Server.Instance.Logices.VoidLeviathan.OnPlayerDisconnected(this);

            this.SaveToDisk();

            ServerModel.PlayerDisconnectedArgs packet = new ServerModel.PlayerDisconnectedArgs()
            {
                UniqueId = this.UniqueId
            };

            this.SendPacketToOtherClients(packet);
        }

        public void AddUsedTool(TechType techType)
        {
            if (!this.UsedTools.Contains(techType))
            {
                this.UsedTools.Add(techType);
            }
        }

        public string GetBiome()
        {
            return LargeWorld.main.GetBiome(this.Position.ToVector3());
        }

        public void SetInVoidBiome(bool isInVoidBiome)
        {
            this.IsInVoidBiome = isInVoidBiome;
        }

        public void SetVehicle(string vehicleId)
        {
            this.VehicleId = vehicleId;
        }

        public void SetSubroot(string subrootId)
        {
            this.SubrootId = subrootId;
        }

        public void SetInterior(string interiorId)
        {
            this.InteriorId = interiorId;
        }

        public void SetRespawnPointId(string respawnPointId)
        {
            this.RespawnPointId = respawnPointId;
        }

        public void SetPosition(ZeroVector3 position, ZeroQuaternion rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        public void SetLastHypnotizeTime(float lastHypnotizeTime)
        {
            this.LastHypnotizeTime = lastHypnotizeTime + 30f;
        }

        public bool AddInventoryItem(StorageItem item)
        {
            this.RemoveInventoryItem(item.ItemId);
            this.InventoryItems.AddItem(item);
            return true;
        }

        public void RemoveInventoryItem(string itemId)
        {
            this.InventoryItems.RemoveItem(itemId);
        }

        public bool CompleteGoal(string storyKey, GoalType goalType, bool isPlayMuted)
        {
            return this.SpecialGoals.Add(new ZeroStoryGoal()
            {
                Key = storyKey,
                GoalType = goalType,
                IsPlayMuted = isPlayMuted,
                FinishedTime = Server.Instance.Logices.World.GetServerTime(),
            });
        }

        public bool IsHypnotized()
        {
            return this.LastHypnotizeTime > Server.Instance.Logices.World.GetServerTime();
        }

        public bool IsInventoryItemExists(string uniqueId)
        {
            return this.InventoryItems.IsItemExists(uniqueId);
        }

        public void SetUsingRoomId(string constructionId)
        {
            this.UsingRoomId = constructionId;
        }

        public void SetUnderAttack(float attackTime)
        {
            this.LastAttackTime = Server.Instance.Logices.World.GetServerTime() + attackTime;
        }

        public bool IsUnderAttack()
        {
            return this.LastAttackTime > Server.Instance.Logices.World.GetServerTime();
        }

        public void SetStoryCinematicMode(bool isActive)
        {
            this.IsStoryCinematicModeActive = isActive;
        }

        public void SetEquipments(byte[] equipments, Dictionary<string, string> equipmentSlots)
        {
            this.Equipments = equipments;
            this.EquipmentSlots = equipmentSlots;
        }

        public void SetQuickSlots(string[] slots)
        {
            this.QuickSlots = slots;
        }

        public void SetActiveSlot(int activeSlot)
        {
            this.ActiveSlot = activeSlot;
        }

        public void SetPinItems(List<TechType> itemPins)
        {
            this.ItemPins = itemPins;
        }

        public void SetWeatherProfile(string profileId)
        {
            this.WeatherProfileId = profileId;
        }

        public void RemoveNotification(string key)
        {
            this.PdaNotifications.RemoveWhere(q => q.Key == key);
        }

        public void AddNotification(NotificationManager.Group group, string key, bool isAdded)
        {
            var notification = this.PdaNotifications.FirstOrDefault(q => q.Key == key);
            if (notification == null)
            {
                this.PdaNotifications.Add(new NotificationItem(group, key, !isAdded, false, true, 0));
            }
            else
            {
                if (!notification.IsViewed && !isAdded)
                {
                    notification.IsViewed = true;
                }
            }
        }

        public void SetNotificationVisible(string uniqueId, bool isVisible)
        {
            var notification = this.PdaNotifications.FirstOrDefault(q => q.Key == uniqueId);
            if (notification == null)
            {
                this.PdaNotifications.Add(new NotificationItem(NotificationManager.Group.Undefined, uniqueId, true, true, isVisible, 0));
            }
            else
            {
                notification.IsPing = true;
                notification.IsVisible = isVisible;
            }

        }

        public void SetNotificationColorIndex(string uniqueId, sbyte colorIndex)
        {
            var notification = this.PdaNotifications.FirstOrDefault(q => q.Key == uniqueId);
            if (notification == null)
            {
                this.PdaNotifications.Add(new NotificationItem(NotificationManager.Group.Undefined, uniqueId, true, true, true, colorIndex));
            }
            else
            {
                notification.IsPing = true;
                notification.ColorIndex = colorIndex;
            }

        }

        public void SetHealth(float health)
        {
            this.Health = health;
        }

        public void SetFood(float food)
        {
            this.Food = food;
        }

        public void SetWater(float water)
        {
            this.Water = water;
        }

        public bool CanSeeTheCreature(MultiplayerCreatureItem creature, bool longDistance = false)
        {
            return this.Position.Distance(creature.Position) < creature.Data.GetVisibilityDistance(longDistance);
        }

        public void SendPacket(NetworkPacket packet)
        {
            Server.SendPacket(this, packet);
        }

        public void SendPacketToAllClient(NetworkPacket packet, bool checkConnected = false)
        {
            Server.SendPacketToAllClient(packet, checkConnected);
        }

        public void SendPacketToOtherClients(NetworkPacket packet, bool checkConnected = false)
        {
            Server.SendPacketToOtherClients(this, packet, checkConnected);
        }

        public bool SaveToDisk()
        {
            lock (Server.Instance.Storages.Player.ProcessLock)
            {
                var data = NetworkTools.Serialize(this);
                if (data == null)
                {
                    Log.Error(string.Format("Player.SaveToDisk -> Error Code (0x01): {0}", this.UniqueId));
                    return false;
                }

                if (!data.IsValid())
                {
                    Log.Error(string.Format("Player.SaveToDisk -> Error Code (0x02): {0}", this.UniqueId));
                    return false;
                }

                return data.WriteToDisk(Server.Instance.Storages.Player.GetPlayerFilePath(this.UniqueId));
            }
        }
    }
}
