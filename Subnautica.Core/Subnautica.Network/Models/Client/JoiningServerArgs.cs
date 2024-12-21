namespace Subnautica.Network.Models.Client
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.Construction;
    using Subnautica.Network.Models.Storage.Player;
    using Subnautica.Network.Models.Storage.Story.StoryGoals;
    using Subnautica.Network.Models.Storage.Technology;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;
    using System.Collections.Generic;
    using Metadata = Subnautica.Network.Models.Metadata;
    using StoryStorage = Subnautica.Network.Models.Storage.Story;
    using WorldChildrens = Subnautica.Network.Models.Storage.World.Childrens;

    [MessagePackObject]
    public class JoiningServerArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.JoiningServer;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.Startup;

        [Key(5)]
        public string PlayerUniqueId { get; set; }

        [Key(6)]
        public string PlayerSubRootId { get; set; }

        [Key(7)]
        public string ServerId { get; set; }

        [Key(8)]
        public float PlayerHealth { get; set; }

        [Key(9)]
        public float PlayerWater { get; set; }

        [Key(10)]
        public float PlayerFood { get; set; }

        [Key(11)]
        public ZeroVector3 PlayerPosition { get; set; }

        [Key(12)]
        public ZeroQuaternion PlayerRotation { get; set; }

        [Key(13)]
        public Metadata.StorageContainer PlayerInventoryItems { get; set; }

        [Key(14)]
        public byte[] PlayerEquipments { get; set; }

        [Key(15)]
        public Dictionary<string, string> PlayerEquipmentSlots { get; set; }

        [Key(16)]
        public string[] PlayerQuickSlots { get; set; }

        [Key(17)]
        public int PlayerActiveSlot { get; set; }

        [Key(18)]
        public List<TechType> PlayerItemPins { get; set; }

        [Key(19)]
        public HashSet<NotificationItem> PlayerNotifications { get; set; }

        [Key(20)]
        public Dictionary<TechType, TechnologyItem> Technologies { get; set; } = new Dictionary<TechType, TechnologyItem>();

        [Key(21)]
        public HashSet<TechType> ScannedTechnologies { get; set; } = new HashSet<TechType>();

        [Key(22)]
        public HashSet<TechType> AnalizedTechnologies { get; set; } = new HashSet<TechType>();

        [Key(23)]
        public HashSet<string> Encyclopedias { get; set; } = new HashSet<string>();

        [Key(24)]
        public HashSet<TechType> PlayerUsedTools { get; set; } = new HashSet<TechType>();

        [Key(25)]
        public HashSet<ConstructionItem> Constructions { get; set; } = new HashSet<ConstructionItem>();

        [Key(26)]
        public byte[] ConstructionRoot { get; set; }

        [Key(27)]
        public List<string> JukeboxDisks { get; set; }

        [Key(28)]
        public Dictionary<string, string> InteractList { get; set; }

        [Key(29)]
        public float ServerTime { get; set; }

        [Key(30)]
        public bool IsFirstLogin { get; set; }

        [Key(31)]
        public GameModePresetId GameMode { get; set; }

        [Key(32)]
        public List<PlayerItem> Players { get; set; }

        [Key(33)]
        public Dictionary<string, NetworkWorldEntityComponent> PersistentEntities { get; set; }

        [Key(34)]
        public HashSet<WorldDynamicEntity> DynamicEntities { get; set; }

        [Key(35)]
        public float PlayerTimeLastSleep { get; set; }

        [Key(36)]
        public bool IsStartedGame { get; set; }

        [Key(37)]
        public List<WorldChildrens.SupplyDrop> SupplyDrops { get; set; }

        [Key(38)]
        public string PlayerInteriorId { get; set; }

        [Key(39)]
        public List<Base> Bases { get; set; }

        [Key(40)]
        public Metadata.StorageContainer QuantumLocker { get; set; }

        [Key(41)]
        public byte PlayerId { get; set; }

        [Key(42)]
        public byte MaxPlayerCount { get; set; }

        [Key(43)]
        public Dictionary<string, string> SeaTruckConnections { get; set; }

        [Key(44)]
        public StoryStorage.Story Story { get; set; }

        [Key(45)]
        public List<string> ActivatedTeleporters { get; set; }

        [Key(46)]
        public HashSet<ZeroStoryGoal> PlayerSpecialGoals { get; set; }

        [Key(47)]
        public HashSet<Brinicle> Brinicles { get; set; }

        [Key(48)]
        public HashSet<CosmeticItem> CosmeticItems { get; set; }

        [Key(49)]
        public HashSet<TechType> DiscoveredTechTypes { get; set; } = new HashSet<TechType>();

        [Key(50)]
        public string PlayerRespawnPointId { get; set; }

        [Key(51)]
        public bool IsInitialEquipmentAdded { get; set; }

        [Key(52)]
        public float PlayerHypnotizeTime { get; set; }
    }
}
