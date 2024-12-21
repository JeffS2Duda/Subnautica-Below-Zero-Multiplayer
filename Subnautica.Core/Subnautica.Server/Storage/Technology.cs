namespace Subnautica.Server.Storage
{
    using System;
    using System.IO;

    using Subnautica.API.Features;
    using Subnautica.Network.Core;
    using Subnautica.Network.Models.Storage.Technology;
    using Subnautica.Server.Abstracts;

    using TechnologyStorage = Network.Models.Storage.Technology;

    public class Technology : BaseStorage
    {
        public TechnologyStorage.Technology Storage { get; set; }

        public override void Start(string serverId)
        {
            this.ServerId = serverId;
            this.FilePath = Paths.GetMultiplayerServerSavePath(this.ServerId, "Technology.bin");
            this.InitializePath();
            this.Load();
        }

        public override void Load()
        {
            if (File.Exists(this.FilePath))
            {
                lock(this.ProcessLock)
                {
                    try
                    {
                        this.Storage = NetworkTools.Deserialize<TechnologyStorage.Technology>(File.ReadAllBytes(this.FilePath));
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Technology.Load: {e}");
                    }
                }
            }
            else
            {
                this.Storage = new TechnologyStorage.Technology();
                this.SaveToDisk();
            }

            if (Core.Server.DEBUG)
            {
                Log.Info("Technologies: ");
                Log.Info("---------------------------------------------------------------");
                foreach (var item in this.Storage.Technologies)
                {
                    Log.Info(String.Format("{0}: {1}/{2}", item.Key, item.Value.Unlocked, item.Value.TotalFragment));
                }
                Log.Info("---------------------------------------------------------------");
            }
        }

        public override void SaveToDisk()
        {
            lock (this.ProcessLock)
            {
                this.WriteToDisk(this.Storage);
            }
        }

        public bool AddTechnology(TechnologyItem technology)
        {
            lock (this.ProcessLock)
            {
                if (this.Storage.Technologies.TryGetValue(technology.TechType, out var temp))
                {
                    if (temp.Unlocked == temp.TotalFragment)
                    {
                        return false;
                    }

                    this.Storage.Technologies[technology.TechType] = technology;
                    return true;
                }

                this.Storage.Technologies.Add(technology.TechType, technology);
                return true;
            }
        }

        public TechnologyItem GetTechnology(TechType techType, int totalFragment)
        {
            lock (this.ProcessLock)
            {
                if (this.Storage.Technologies.TryGetValue(techType, out var temp))
                {
                    return temp;
                }

                return new TechnologyItem()
                {
                    TechType      = techType,
                    Unlocked      = 0,
                    TotalFragment = totalFragment,
                };
            }
        }

        public bool AddAnalyzedTechnology(TechType techType)
        {
            lock (this.ProcessLock)
            {
                if (this.Storage.AnalizedTechnologies.Contains(techType))
                {
                    return false;
                }

                this.Storage.AnalizedTechnologies.Add(techType);
                return true;
            }
        }
    }
}
