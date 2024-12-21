namespace Subnautica.Server.Logic.Furnitures
{
    using System.Collections.Generic;
    using System.Linq;

    using Subnautica.API.Features;
    using Subnautica.Network.Models.Storage.Construction;
    using Subnautica.Server.Abstracts;

    using UnityEngine;

    using Metadata    = Subnautica.Network.Models.Metadata;
    using ServerModel = Subnautica.Network.Models.Server;

    public class Hoverpad : BaseLogic
    {
        private Dictionary<string, List<string>> PlayersOnPlatform { get; set; } = new Dictionary<string, List<string>>();

        public float HoverbikeEnergyCapacity { get; set; } = 100f;

        public StopwatchItem Timing { get; set; } = new StopwatchItem(1000f);

        private List<ConstructionItem> Requests { get; set; } = new List<ConstructionItem>();

        public override void OnStart()
        {
            foreach (var construction in this.GetHoverpads())
            {
                construction.Value.EnsureComponent<Metadata.Hoverpad>().ShowroomPlayerCount = 0;
            }
        }

        public override void OnUpdate(float deltaTime)
        {
            if (this.Timing.IsFinished() && World.IsLoaded)
            {
                this.Timing.Restart();

                this.Requests.Clear();

                foreach (var construction in this.GetHoverpads())
                {
                    var hoverpad = construction.Value.EnsureComponent<Metadata.Hoverpad>();
                    if (hoverpad.IsDocked)
                    {
                        hoverpad.Hoverbike.Charge = Mathf.Min(this.HoverbikeEnergyCapacity, hoverpad.Hoverbike.Charge + 1f);
                        hoverpad.Hoverbike.LiveMixin.AddHealth(5f);

                        this.Requests.Add(construction.Value);
                    }
                }

                this.SendPacketToAllClient();
            }
        }

        private void SendPacketToAllClient()
        {
            if (this.Requests.Count > 0)
            {
                foreach (var profile in Core.Server.Instance.GetPlayers())
                {
                    var request = new ServerModel.HoverpadChargeTransmissionArgs()
                    {
                        Items = new Dictionary<uint, ServerModel.HoverpadEnergyTransmissionItem>()
                    };

                    foreach (var item in this.Requests)
                    {
                        if (item.PlacePosition.Distance(profile.Position) > 100f)
                        {
                            continue;
                        }

                        var hoverpad = item.EnsureComponent<Metadata.Hoverpad>();
                        if (hoverpad == null)
                        {
                            continue;
                        }

                        request.Items.Add(item.Id, new ServerModel.HoverpadEnergyTransmissionItem((byte) hoverpad.Hoverbike.LiveMixin.Health, (byte)hoverpad.Hoverbike.Charge));
                    }

                    if (request.Items.Any())
                    {
                        profile.SendPacket(request);
                    }
                }
            }
        }

        public byte GetPlayerCountFromPlatform(string constructionId)
        {
            if (this.PlayersOnPlatform.TryGetValue(constructionId, out var players))
            {
                return (byte) players.Count;
            }

            return 0;
        }

        public void AddPlayerToPlatform(string constructionId, string playerId)
        {
            if (!this.PlayersOnPlatform.ContainsKey(constructionId))
            {
                this.PlayersOnPlatform.Add(constructionId, new List<string>());
            }

            if (!this.PlayersOnPlatform[constructionId].Contains(playerId))
            {
                this.PlayersOnPlatform[constructionId].Add(playerId);
            }
        }

        public void RemovePlayerFromPlatform(string constructionId, string playerId)
        {
            if (this.PlayersOnPlatform.TryGetValue(constructionId, out var players))
            {
                players.Remove(playerId);
            }
        }

        public void RemovePlayerFromPlatform(string playerId, bool autoSend)
        {
            foreach (var platform in this.PlayersOnPlatform)
            {
                if (platform.Value.Contains(playerId))
                {
                    platform.Value.Remove(playerId);

                    if (autoSend)
                    {
                        ServerModel.MetadataComponentArgs result = new ServerModel.MetadataComponentArgs()
                        {
                            UniqueId  = platform.Key,
                            TechType  = TechType.Hoverpad,
                            Component = new Metadata.Hoverpad()
                            {
                                ShowroomTriggerType = 1,
                                ShowroomPlayerCount = (byte) platform.Value.Count,
                            },
                        };

                        Core.Server.SendPacketToAllClient(result);
                    }
                }
            }
        }

        public List<KeyValuePair<string, ConstructionItem>> GetHoverpads()
        {
            return Core.Server.Instance.Storages.Construction.Storage.Constructions.Where(q => q.Value.ConstructedAmount == 1f && q.Value.TechType == TechType.Hoverpad).ToList();
        }
    }
}
