namespace Subnautica.API.Features
{
    using System;
    using System.Net.Http;
    using System.Net.NetworkInformation;
    using System.Text;

    public class NetBirdApi
    {
        public string LobbyURL { get; set; } = (string)Settings.ModConfig.LobbyURL.Value;

        private Ping Ping { get; set; }

        private PingOptions PingOptions { get; set; }

        private string InstallationPath { get; set; } = null;

        private string Id { get; set; } = "";

        private static NetBirdApi instance;

        public static NetBirdApi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NetBirdApi();
                    instance.Initialize();
                }

                return instance;
            }
        }

        public void Initialize()
        {
            this.Ping = new Ping();
            this.PingOptions = new PingOptions();
            this.PingOptions.DontFragment = true;
        }

        public void SetupNetbirdWithAdminPerms()
        {

        }

        public bool IsCommandRunning()
        {
            return false;
        }

        public bool IsConnectingToNetwork()
        {
            return false;
        }

        public bool IsExists()
        {
            return true;
        }

        public void Refresh()
        {

        }

        public void Disconnect()
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new(LobbyURL);
            httpClient.PostAsync("disconnect", new StringContent(Id));
            Id = string.Empty;
        }

        public bool Connect()
        {
            Log.Info("Connecting to Lobby Server!");
            Log.Info($"Connecting to: {LobbyURL}");
            string id = Tools.GetLoggedId();
            Log.Info($"Logging with ID: {id}");
            Log.Info($"Logging with IP: {(string)Settings.ModConfig.MyIp.Value}");
            Log.Info($"Logging with Port: {Settings.ModConfig.HostOnPort.GetInt()}");
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new(LobbyURL);
            var post = httpClient.PostAsync("connect", new StringContent(id + " | " + (string)Settings.ModConfig.MyIp.Value + " | " + Settings.ModConfig.HostOnPort.GetInt())).Result;
            Id = post.Content.ReadAsStringAsync().Result;
            return this.IsReady();
        }

        public bool StartInstall()
        {
            return true;
        }

        public string GetPeerId()
        {
            return Id;
        }

        public string GetPeerIp()
        {
            return (string)Settings.ModConfig.MyIp.Value;
        }

        public bool IsWaitingInstallation()
        {
            return false;
        }

        public bool IsWaitingLogin()
        {
            return string.IsNullOrEmpty(Id);
        }

        public bool IsAnyError()
        {
            return false;
        }

        public bool IsReady()
        {
            if (this.IsWaitingLogin())
            {
                return false;
            }
            return true;
        }

        public bool IsHostConnectionActive(string hostIp)
        {
            try
            {
                var status = this.Ping.Send(hostIp, 1000, Encoding.ASCII.GetBytes(System.Guid.NewGuid().ToString()), this.PingOptions).Status;
                return status == IPStatus.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsHostConnected(string hostIp)
        {
            Log.Info($"IsHostConnected: {hostIp}");
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(LobbyURL);
            var post = httpClient.GetAsync($"/checkhost?hostIp={hostIp}").Result;
            return bool.Parse(post.Content.ReadAsStringAsync().Result);
        }

        public bool RemoveAndUpdateInstall()
        {
            return true;
        }

        public void ExecuteCommand(string command, bool isInstallion = false, int timeout = 10000)
        {

        }
    }
}