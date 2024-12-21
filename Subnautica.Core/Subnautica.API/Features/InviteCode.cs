namespace Subnautica.API.Features
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features.Helper;
    using System;
    using System.Collections;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Networking;

    public class InviteCode
    {
        private string Code { get; set; }

        private string AccessToken { get; set; }

        private bool IsClientConnected { get; set; } = false;

        private bool IsHostConnected { get; set; } = false;

        private string CurrentPeerId { get; set; } = null;

        private string ApiUrl { get; set; } = "https://repo.subnauticamultiplayer.com/api/";

        private StopwatchItem ConnectionTiming { get; set; } = new StopwatchItem(20000f);

        private StopwatchItem HostConnectionTiming { get; set; } = new StopwatchItem(30000f);

        private LobbyJoinServerResponseFormat JoinResponse { get; set; } = null;

        public void SetInviteCode(string inviteCode)
        {
            this.Code = inviteCode;
        }

        public void SetAccessToken(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        public string GetInviteCode()
        {
            return this.Code;
        }

        public string GetAccessToken()
        {
            return this.AccessToken;
        }

        public IEnumerator CheckAndConnect()
        {
            this.IsClientConnected = false;
            this.CurrentPeerId = string.Empty;
            this.ConnectionTiming.Restart();

            Task.Run(() =>
            {
                NetBirdApi.Instance.Refresh();

                if (NetBirdApi.Instance.IsReady())
                {
                    this.IsClientConnected = true;
                }
                else
                {
                    while (NetBirdApi.Instance.IsConnectingToNetwork())
                    {
                        Thread.Sleep(500);
                    }

                    if (NetBirdApi.Instance.IsReady())
                    {
                        this.IsClientConnected = true;
                    }
                    else
                    {
                        Thread.Sleep(500);

                        for (int i = 0; i < 3; i++)
                        {
                            if (this.ConnectionTiming.IsFinished())
                            {
                                break;
                            }

                            this.IsClientConnected = NetBirdApi.Instance.Connect();
                            if (this.IsClientConnected)
                            {
                                break;
                            }
                        }
                    }
                }

                if (this.IsClientConnected)
                {
                    this.CurrentPeerId = NetBirdApi.Instance.GetPeerId();
                }
            });

            while (true)
            {
                if (this.ConnectionTiming.IsFinished())
                {
                    yield break;
                }
                else if (this.CurrentPeerId.IsNotNull() && this.IsClientConnected)
                {
                    yield return new WaitForSecondsRealtime(1f);
                    yield break;
                }
                else
                {
                    yield return new WaitForSecondsRealtime(0.1f);
                }
            }
        }

        public IEnumerator WaitForHostConnection(string hostIp)
        {
            this.IsHostConnected = false;
            this.HostConnectionTiming.Restart();

            var hostCounter = 0;
            var connectCounter = 0;

            Task.Run(() =>
            {
                while (!this.HostConnectionTiming.IsFinished())
                {
                    Thread.Sleep(250);

                    if (!NetBirdApi.Instance.IsHostConnected(hostIp))
                    {
                        Log.Info($"IsHostConnected: ({++connectCounter})");
                        continue;
                    }

                    if (!NetBirdApi.Instance.IsHostConnectionActive(hostIp))
                    {
                        Log.Info($"HOST CONNECTION WAITING: ({++hostCounter})");
                        continue;
                    }

                    this.IsHostConnected = true;
                    break;
                }
            });

            while (true)
            {
                if (this.HostConnectionTiming.IsFinished())
                {
                    yield break;
                }
                else if (this.IsHostConnected)
                {
                    yield return new WaitForSecondsRealtime(0.5f);
                    yield break;
                }
                else
                {
                    yield return new WaitForSecondsRealtime(0.1f);
                }
            }
        }

        public IEnumerator JoinServerAsync(string joinCode, Action<LobbyJoinServerResponseFormat> onSuccess)
        {
            ZeroGame.ShowLoadingScreen();

            yield return this.CheckAndConnect();

            if (this.IsClientConnected && this.CurrentPeerId.IsNotNull())
            {
                using (UnityWebRequest request = UnityWebRequest.Get($"{this.ApiUrl}joinserver?joinCode={Uri.EscapeDataString(joinCode)}&peerId={Uri.EscapeDataString(this.CurrentPeerId)}"))
                {
                    yield return request.SendWebRequest();

                    if (request.downloadHandler.text.IsNull())
                    {
                        this.ShowErrorMessage("API_WEB_SERVER_RETURN_NULL", "JoinServer.Part_2");
                    }
                    else
                    {
                        try
                        {
                            this.JoinResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LobbyJoinServerResponseFormat>(request.downloadHandler.text);
                        }
                        catch (Exception ex)
                        {
                            this.ShowErrorMessage("API_SERIALIZE_ERROR", $"JoinServer.Part_3: {ex.Message}");
                        }

                        if (this.JoinResponse != null)
                        {
                            if (this.JoinResponse.IsError)
                            {
                                this.ShowErrorMessage(this.JoinResponse.ErrorMessage, "JoinServer.Part_4");
                            }
                            else
                            {
                                yield return this.WaitForHostConnection(this.JoinResponse.ServerIp);

                                if (this.IsHostConnected)
                                {
                                    onSuccess?.Invoke(this.JoinResponse);
                                }
                                else
                                {
                                    this.ShowErrorMessage(triggerKey: "JoinServer.Part_5");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                this.ShowErrorMessage(triggerKey: "JoinServer.Part_1");
            }
        }

        public IEnumerator CreateServerAsync(Action<LobbyCreateServerResponse> onSuccess, Action onFinished = null)
        {
            LoadingStage.durations["CreateServer"] = 15f;
            uGUI.main.loading.SetStageProgress("CreateServer", 0.4f);
            ZeroGame.ShowLoadingScreen();

            yield return this.CheckAndConnect();

            if (this.IsClientConnected && this.CurrentPeerId.IsNotNull())
            {
                using (UnityWebRequest request = UnityWebRequest.Get($"{this.ApiUrl}createserver?peerId={Uri.EscapeDataString(this.CurrentPeerId)}"))
                {
                    yield return request.SendWebRequest();

                    if (request.downloadHandler.text.IsNull())
                    {
                        this.ShowErrorMessage("API_WEB_SERVER_RETURN_NULL", "CreateServer.Part_2");
                    }
                    else
                    {
                        try
                        {
                            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<LobbyCreateServerResponse>(request.downloadHandler.text);
                            if (response != null)
                            {
                                if (response.IsError)
                                {
                                    this.ShowErrorMessage(response.ErrorMessage);
                                }
                                else
                                {
                                    this.SetInviteCode(response.JoinCode);
                                    this.SetAccessToken(response.AccessToken);

                                    onSuccess?.Invoke(response);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.ShowErrorMessage("API_SERIALIZE_ERROR", $"CreateServer.Part_3: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                this.ShowErrorMessage(triggerKey: "CreateServer.Part_1");
            }

            onFinished?.Invoke();
        }

        public IEnumerator LeaveFromServerAsync(string peerIp, Action<bool> callback = null)
        {
            if (peerIp.IsNotNull())
            {
                using (UnityWebRequest request = UnityWebRequest.Get($"{this.ApiUrl}leaveserver?peerIp={peerIp}&accessToken={Uri.EscapeDataString(this.GetAccessToken())}"))
                {
                    request.timeout = 5;
                    yield return request.SendWebRequest();

                    if (request.isNetworkError || request.isHttpError)
                    {
                        Log.Error($"LeaveFromServerAsync.Request Error: {request.error}");

                        callback?.Invoke(true);
                    }
                    else
                    {
                        if (request.responseCode != 200)
                        {
                            Log.Error($"LeaveFromServerAsync.ResponseCode Error: {request.downloadHandler.text}");
                            callback?.Invoke(true);
                        }
                        else
                        {
                            try
                            {
                                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<LobbyPingServerResponseFormat>(request.downloadHandler.text);
                                if (response != null)
                                {
                                    if (response.IsError)
                                    {
                                        Log.Error($"LeaveFromServerAsync.Response Error: {response.ErrorMessage}");
                                        callback?.Invoke(true);
                                    }
                                    else
                                    {
                                        callback?.Invoke(false);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error($"LeaveFromServerAsync.Exception Error: {ex}");

                                callback?.Invoke(true);
                            }
                        }
                    }
                }
            }
        }

        public IEnumerator PingToServerAsync(Action<bool> callback)
        {
            using (UnityWebRequest request = UnityWebRequest.Get($"{this.ApiUrl}pingserver?accessToken={Uri.EscapeDataString(this.GetAccessToken())}"))
            {
                request.timeout = 5;
                yield return request.SendWebRequest();

                if (request.isNetworkError || request.isHttpError)
                {
                    Log.Error($"X.Request Error: {request.error}");

                    callback?.Invoke(true);
                }
                else
                {
                    if (request.responseCode != 200)
                    {
                        Log.Error($"X.ResponseCode Error: {request.downloadHandler.text}");
                        callback?.Invoke(true);
                    }
                    else
                    {
                        try
                        {
                            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<LobbyPingServerResponseFormat>(request.downloadHandler.text);
                            if (response != null)
                            {
                                if (response.IsError)
                                {
                                    Log.Error($"X.Response Error: {response.ErrorMessage}");
                                    callback?.Invoke(true);
                                }
                                else
                                {
                                    callback?.Invoke(false);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error($"X.Exception Error: {ex}");

                            callback?.Invoke(true);
                        }
                    }
                }
            }
        }

        private void ShowErrorMessage(string errorKey = null, string triggerKey = null)
        {
            ZeroGame.StopLoadingScreen();

            if (errorKey.IsNull())
            {
                ErrorMessages.ShowConnectionFixErrorMessage();
            }
            else
            {
                ErrorMessages.ShowErrorMessage(errorKey);
            }

            if (triggerKey.IsNotNull())
            {
                Log.Error($"Trigger Error Key: {triggerKey}");
            }
        }

        public void Dispose()
        {
            this.Code = null;
            this.CurrentPeerId = null;
            this.AccessToken = null;
            this.IsClientConnected = false;
            this.IsHostConnected = false;
        }
    }
}
