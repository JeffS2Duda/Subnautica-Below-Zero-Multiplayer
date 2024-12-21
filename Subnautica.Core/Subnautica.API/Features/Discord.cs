namespace Subnautica.API.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using DiscordRPC;

    using Subnautica.API.Features.DiscordManager;

    using UnityEngine;

    public class Discord : MonoBehaviour
    {
        private static DateTime StartedTime { get; set; }

        private static Queue<RichPresence> Queue { get; set; } = new Queue<RichPresence>();

        private static DiscordRpcClient Client { get; set; }

        private static GameObject DiscordManager { get; set; }

        public void Awake()
        {
            this.transform.parent = null;

            DontDestroyOnLoad(this.gameObject);

            if (Client == null)
            {
                Client = new DiscordRpcClient(Settings.DiscordClientId, pipe: -1, logger: null, autoEvents: true, client: new NamedPipeUnity());
                
                Client.OnError += (sender, e) =>
                {
                    Log.Error("Discord.Integration: " + e.Message);
                };

                Client.OnReady += (sender, e) =>
                {
                    Log.Info("Received Ready from user " + e.User.Username + "#" + e.User.Discriminator);
                };

                Client.OnPresenceUpdate += (sender, e) =>
                {

                };

                Client.Initialize();
            }
        }

        public static void UpdateRichPresence(string message, string subMessage = null, bool resetTime = false)
        {
            if (Client == null)
            {
                if (UnityEngine.Object.FindObjectOfType<Discord>() == null)
                {
                    DiscordManager = new GameObject("DiscordManager");
                    DiscordManager.hideFlags = HideFlags.HideAndDontSave;
                    DiscordManager.AddComponent<Discord>();
                    DiscordManager.SetActive(true);

                    resetTime = true;
                }
            }

            if (resetTime)
            {
                StartedTime = DateTime.UtcNow;
            }

            RichPresence rich = new RichPresence()
            {
                Details    = message,
                State      = subMessage,
                Timestamps = new Timestamps(StartedTime),
                Assets     = new Assets()
                {
                    LargeImageKey  = "subnauticabelowzero",
                    LargeImageText = "Subnautica BZ Multiplayer",
                }
            };

            Queue.Enqueue(rich);
        }

        public void Update()
        {
            if (Client != null)
            {
                Client.Invoke();
            }
        }

        public void FixedUpdate()
        {
            if (Client != null)
            {
                while (Queue.Count > 0)
                {
                    var item = Queue.Dequeue();

                    Client.SetPresence(item);
                }
            }
        }
    }
}
