using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Subnautica.API.Extensions;

using UnityEngine;

namespace Subnautica.API.Features.Netbird
{
    public class Netbird
    {
        private bool isWaitingInstallation;

        private bool isWaitingLogin;

        private bool isValidJson;

        private bool isHasError;

        private string peerId;

        private string peerIp;

        private string TextContent;

        private string ErrorContent;

        private NetbirdResponseFormat OutputData;

        public NetbirdContainerItem Management { get; set; } = new NetbirdContainerItem();

        public NetbirdContainerItem Signal { get; set; } = new NetbirdContainerItem();

        public NetbirdContainerItem Relay { get; set; } = new NetbirdContainerItem();

        public NetbirdContainerItem Stun { get; set; } = new NetbirdContainerItem();

        public NetbirdContainerItem Turn { get; set; } = new NetbirdContainerItem();

        public Netbird()
        {

        }

        public Netbird(string textContent, string errorContent)
        {
            this.TextContent  = textContent.Trim();
            this.ErrorContent = errorContent.Trim();

            try
            {
                this.Initialize();
            }
            catch (Exception ex)
            {
                Log.Error("DATA:::: err: " + ex);
            }
        }

        private bool Initialize()
        {
            this.isWaitingInstallation = this.ErrorContent.Contains("service install");
            this.isWaitingLogin        = this.TextContent.Contains("Daemon status: NeedsLogin");
            this.isValidJson           = this.CheckValidJson(this.TextContent);
            this.isHasError            = this.ErrorContent.IsNotNull();

            if (this.TextContent.IsNull())
            {
                return false;
            }

            if (this.IsWaitingInstallation() || this.IsWaitingLogin() || !this.IsValidJson())
            {
                return false;
            }

            try
            {
                this.OutputData = Newtonsoft.Json.JsonConvert.DeserializeObject<NetbirdResponseFormat>(this.TextContent);
                if (this.OutputData == null)
                {
                    this.isValidJson = false;
                    return false;
                }

                if (this.OutputData.NetbirdIp.IsNotNull())
                {
                    this.SetPeerIp(this.OutputData.NetbirdIp);
                }

                if (this.OutputData.PublicKey.IsNotNull())
                {
                    this.SetPeerId(this.OutputData.PublicKey);
                }

                if (this.OutputData.Management != null)
                {
                    this.Management.SetConnected(this.OutputData.Management.Connected);
                    this.Management.SetErrorMessage(this.OutputData.Management.Error);
                }

                if (this.OutputData.Signal != null)
                {
                    this.Signal.SetConnected(this.OutputData.Signal.Connected);
                    this.Signal.SetErrorMessage(this.OutputData.Signal.Error);
                }

                if (this.OutputData.Relays != null && this.OutputData.Relays.Details != null)
                {
                    foreach (var relay in this.OutputData.Relays.Details)
                    {
                        if (relay.Uri.Contains("rels://") || relay.Uri.Contains("rel://") || relay.Uri == "")
                        {
                            this.Relay.SetConnected(relay.Available);
                            this.Relay.SetErrorMessage(relay.Error);
                        }
                        else if (relay.Uri.Contains("turn:"))
                        {
                            this.Turn.SetConnected(relay.Available);
                            this.Turn.SetErrorMessage(relay.Error);
                        }
                        else if (relay.Uri.Contains("stun:"))
                        {
                            this.Stun.SetConnected(relay.Available);
                            this.Stun.SetErrorMessage(relay.Error);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("ex: " + ex);
                this.isValidJson = false;
                return false;
            }
        }

        public bool IsWaitingInstallation()
        {
            return this.isWaitingInstallation;
        }

        public bool IsWaitingLogin()
        {
            return this.isWaitingLogin;
        }

        public bool IsValidJson()
        {
            return this.isValidJson;
        }

        public bool IsAnyError()
        {
            return this.isHasError;
        }

        public string GetPeerIp()
        {
            return this.peerIp;
        }

        public string GetPeerId()
        {
            return this.peerId;
        }

        private void SetPeerIp(string ipAddress)
        {
            this.peerIp = ipAddress.Replace("/16", "").Replace("/24", "").Replace("/32", "").Trim();
            if (this.peerIp.Contains("N/A"))
            {
                this.peerIp = "";
            }
        }

        private void SetPeerId(string peerId)
        {
            this.peerId = peerId;
        }

        public string GetOutputDataContent()
        {
            return this.TextContent;
        }

        public string GetErrorContent()
        {
            return this.ErrorContent;
        }

        private bool CheckValidJson(string content)
        {
            return content.IsNotNull() && content.StartsWith("{") && content.EndsWith("}");
        }
    }
}
