namespace Subnautica.API.Features.Helper
{
    using Newtonsoft.Json;
    using System;
    using System.IO;

    public class ModConfigFormat
    {
        public ModConfigFormatItem ConnectionTimeout { get; set; } = new ModConfigFormatItem(120, "Connection timeout period. (Type: Number/Second, Default: 120, Min: 60, Max: 300)");

        public ModConfigFormatItem LobbyURL { get; set; } = new("http://192.168.3.50/", "URL/IP to other users to connect to a lobby.");
        public ModConfigFormatItem MyIp { get; set; } = new("192.168.3.50", "Ip address to share to users.");
        public ModConfigFormatItem HostOnPort { get; set; } = new(7777, "Port to host the game on.");
        public ModConfigFormatItem MaxPlayer { get; set; } = new(8, "How many player should max join.");
        public ModConfigFormatItem DefaultJoinPort { get; set; } = new(7777, "Port to host the game on.");

        public void Initialize()
        {
            var filePath = Paths.GetLauncherGameCorePath("Config.json");
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(this, Formatting.Indented));
            }

            try
            {
                var config = JsonConvert.DeserializeObject<ModConfigFormat>(File.ReadAllText(filePath));
                if (config.ConnectionTimeout.GetInt() >= 60 && config.ConnectionTimeout.GetInt() <= 300)
                {
                    this.ConnectionTimeout.SetValue(config.ConnectionTimeout.GetInt());
                }
                LobbyURL.SetValue(config.LobbyURL.Value);
                MyIp.SetValue(config.MyIp.Value);
                HostOnPort.SetValue(config.HostOnPort.Value);
                MaxPlayer.SetValue(config.MaxPlayer.Value);
                DefaultJoinPort.SetValue(config.DefaultJoinPort.Value);
            }
            catch (Exception ex)
            {
                Log.Error($"ModConfigFormat.Initialize - Exception: {ex}");
            }
        }
    }

    public class ModConfigFormatItem
    {
        public string Description { get; set; }

        public object Value { get; set; }

        public ModConfigFormatItem(object value, string description)
        {
            this.Value = value;
            this.Description = description;
        }

        public void SetValue(object value)
        {
            this.Value = value;
        }

        public int GetInt(int defaultValue = -1)
        {
            try
            {
                return Convert.ToInt32(this.Value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
