namespace Subnautica.API.Features
{
    using System;

    using Newtonsoft.Json;

    public class LocalServerItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string IpAddress { get; set; }

        public int Port { get; set; }
    }

    public class HostServerItem
    {
        [JsonIgnore]
        public string Id { get; set; }

        public int GameMode { get; set; }

        public int CreationDate { get; set; }

        public int LastPlayedDate { get; set; }

        public bool IsValidGameMode()
        {
            foreach (int item in Enum.GetValues(typeof(GameModePresetId)))
            {
                if (item == this.GameMode)
                {
                    return true;
                }
            }

            return false;
        }

        public GameModePresetId GetGameMode()
        {
            return (GameModePresetId)this.GameMode;
        }
    }
}
