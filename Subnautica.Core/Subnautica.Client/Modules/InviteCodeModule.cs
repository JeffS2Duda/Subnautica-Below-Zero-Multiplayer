namespace Subnautica.Client.Modules
{
    using Subnautica.API.Features;
    using Subnautica.Events.EventArgs;
    using System;
    using System.Threading.Tasks;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public static class InviteCodeModule
    {
        public static void OnPluginEnabled()
        {
            Application.wantsToQuit += InviteCodeModule.OnWantsToQuit;

            //ConnectToNetwork();
        }

        public static void OnSceneLoaded(SceneLoadedEventArgs ev)
        {
            if (ev.Scene.name == "XMenu")
            {
                ConnectToNetwork();
            }
        }

        public static void ConnectToNetwork()
        {
            Network.InviteCode.SetInviteCode(null);
            Network.InviteCode.SetAccessToken(null);

            if (!NetBirdApi.Instance.IsConnectingToNetwork())
            {
                Task.Run(() =>
                {
                    if (!NetBirdApi.Instance.IsConnectingToNetwork())
                    {
                        NetBirdApi.Instance.Refresh();

                        if (!NetBirdApi.Instance.IsReady())
                        {
                            NetBirdApi.Instance.Connect();
                        }
                    }
                }).ContinueWith((t) =>
                {
                    if (t.IsFaulted)
                    {
                        Log.Error($"ConnectToNetwork Ex: {t.Exception}");
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public static bool OnWantsToQuit()
        {
            try
            {
                NetBirdApi.Instance.Disconnect();
            }
            catch (Exception)
            {

            }

            return true;
        }

        public static void OnQuittingToMainMenu(QuittingToMainMenuEventArgs ev)
        {
            if (Network.IsHost)
            {
                UWE.CoroutineHost.StartCoroutine(Network.InviteCode.LeaveFromServerAsync(NetBirdApi.Instance.GetPeerIp()));
            }
        }

        public static void OnInGameMenuOpened(InGameMenuOpenedEventArgs ev)
        {
            if (Network.IsMultiplayerActive && Network.IsHost)
            {
                var feedbackBtn = IngameMenu.main.transform.Find("Main/ButtonLayout/ButtonFeedback").gameObject;
                if (feedbackBtn.activeSelf)
                {
                    CreateInviteCodeButtons();

                    IngameMenu.main.feedbackButton.gameObject.SetActive(false);
                    IngameMenu.main.transform.Find("Main/ButtonLayout/ButtonFeedback").gameObject.SetActive(false);
                }

                foreach (var item in IngameMenu.main.helpButton.transform.parent.gameObject.GetComponentsInChildren<Button>())
                {
                    if (item.name.Contains("InviteCodeTextButton"))
                    {
                        item.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
                    }
                }
            }
        }

        private static void CreateInviteCodeButtons()
        {
            var inviceCodeButtonShow = GameObject.Instantiate(IngameMenu.main.helpButton.gameObject, IngameMenu.main.helpButton.transform.parent);
            var inviceCodeButtonText = GameObject.Instantiate(IngameMenu.main.helpButton.gameObject, IngameMenu.main.helpButton.transform.parent);
            inviceCodeButtonShow.gameObject.name = "InviteCodeShowButton";
            inviceCodeButtonText.gameObject.name = "InviteCodeTextButton";

            inviceCodeButtonShow.SetActive(true);
            inviceCodeButtonText.SetActive(true);

            inviceCodeButtonShow.GetComponentInChildren<TextMeshProUGUI>().text = ZeroLanguage.Get("GAME_SHOW_INVITE_CODE");
            inviceCodeButtonText.GetComponentInChildren<TextMeshProUGUI>().text = "";

            inviceCodeButtonText.GetComponent<RectTransform>().SetAsFirstSibling();
            inviceCodeButtonShow.GetComponent<RectTransform>().SetAsFirstSibling();

            if (inviceCodeButtonText.TryGetComponent<Button>(out var textBtn))
            {
                textBtn.enabled = false;
                textBtn.GetComponent<Image>().enabled = false;
            }

            if (inviceCodeButtonShow.TryGetComponent<Button>(out var showBtn))
            {
                showBtn.onClick = new Button.ButtonClickedEvent();
                showBtn.onClick.AddListener(() =>
                {
                    inviceCodeButtonText.GetComponentInChildren<TextMeshProUGUI>().text = Network.InviteCode.GetInviteCode();
                });
            }
        }
    }
}
