namespace Subnautica.Server.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Subnautica.API.Features;
    using Subnautica.Network.Core;
    using Subnautica.Server.Abstracts;

    using EncyclopediaStorage = Network.Models.Storage.Encyclopedia;

    public class Encyclopedia : BaseStorage
    {
        public EncyclopediaStorage.Encyclopedia Storage { get; set; }

        public override void Start(string serverId)
        {
            this.ServerId = serverId;
            this.FilePath = Paths.GetMultiplayerServerSavePath(this.ServerId, "Encyclopedia.bin");
            this.InitializePath();
            this.Load();
        }

        public override void Load()
        {
            if (File.Exists(this.FilePath))
            {
                try
                {
                    this.Storage = NetworkTools.Deserialize<EncyclopediaStorage.Encyclopedia>(File.ReadAllBytes(this.FilePath));
                }
                catch (Exception e)
                {
                    Log.Error($"Encyclopedia.Load: {e}");
                }
            }
            else
            {
                this.Storage = new EncyclopediaStorage.Encyclopedia();
                this.SaveToDisk();
            }

            if (Core.Server.DEBUG)
            {
                Log.Info("Encyclopedias: ");
                Log.Info("---------------------------------------------------------------");
                foreach (var item in this.Storage.Encyclopedias)
                {
                    Log.Info(item);
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

        public bool AddEncyclopedia(string encyclopedia)
        {
            lock (this.ProcessLock)
            {
                if (!this.Storage.Encyclopedias.Contains(encyclopedia))
                {
                    this.Storage.Encyclopedias.Add(encyclopedia);
                    return true;
                }

                return false;
            }
        }

        public bool AddPlayerViewedEncyclopedia(string playerName, string encyclopedia)
        {
            lock (this.ProcessLock)
            {
                if (!this.Storage.Encyclopedias.Contains(encyclopedia))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(playerName))
                {
                    return false;
                }

                if (this.Storage.Players.TryGetValue(playerName, out var keys))
                {
                    if (!keys.ContainsKey(encyclopedia))
                    {
                        keys.Add(encyclopedia, true);
                        return true;
                    }

                    return false;
                }

                this.Storage.Players.Add(playerName, new Dictionary<string, bool>() { { encyclopedia, true } });
                return true;
            }
        }
    }
}
