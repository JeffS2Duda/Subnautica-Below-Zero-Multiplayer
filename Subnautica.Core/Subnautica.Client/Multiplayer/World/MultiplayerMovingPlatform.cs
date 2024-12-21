namespace Subnautica.Client.Multiplayer.World
{
    using System.Collections.Generic;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;

    using UnityEngine;

    public class MultiplayerMovingPlatform : MonoBehaviour
    {
        public Transform Platform { get; set; }

        private float PlayerHeight { get; set; } = 0f;

        private Dictionary<string, ZeroPlayer> PlayersInRange = new Dictionary<string, ZeroPlayer>();

        private Queue<string> RemovePlayerQueue = new Queue<string>();

        private string UniqueId { get; set; }

        public void Awake()
        {
            this.PlayerHeight = 1.75f + 0.1f;
        }

        public void SetPlatform(Transform platform)
        {
            this.Platform = platform;
            this.UniqueId = Network.Identifier.GetIdentityId(this.Platform.GetComponentInParent<MultiplayerMovingPlatform>().gameObject, false);
        }

        public void LateUpdate()
        {
            foreach (var player in this.PlayersInRange)
            {
                if (player.Value?.PlayerModel)
                {
                    player.Value.Position = new Vector3(player.Value.Position.x, this.Platform.position.y + this.PlayerHeight, player.Value.Position.z);
                    player.Value.PlayerModel.transform.position = new Vector3(player.Value.PlayerModel.transform.position.x, player.Value.Position.y, player.Value.PlayerModel.transform.position.z);
                }
                else
                {
                    this.RemovePlayerQueue.Enqueue(player.Key);
                }
            }

            while (this.RemovePlayerQueue.Count > 0)
            {
                var playerUniqueId = this.RemovePlayerQueue.Dequeue();
                if (playerUniqueId.IsNotNull())
                {
                    this.PlayersInRange.Remove(playerUniqueId);
                }
            }
        }

        public void FixedUpdate()
        {
            if (this.IsInitialized())
            {
                foreach (var player in ZeroPlayer.GetPlayers())
                {
                    if (this.IsPlayerOnPlatform(player.Position))
                    {
                        this.PlayersInRange[player.UniqueId] = player;
                    }
                    else
                    {
                        this.PlayersInRange.Remove(player.UniqueId);
                    }
                }
            }
        }

        private bool IsPlayerOnPlatform(Vector3 position)
        {
            var raycasts = Physics.RaycastAll(position, Vector3.down, 3f);
            if (raycasts.Length <= 0)
            {
                return false;
            }

            foreach (var item in raycasts)
            {
                if (item.collider == null || item.collider.gameObject.name.Contains("Player"))
                {
                    continue;
                }

                var movingPlatform = item.collider.GetComponentInParent<MultiplayerMovingPlatform>();
                if (movingPlatform == null)
                {
                    continue;
                }

                if (this.UniqueId == Network.Identifier.GetIdentityId(movingPlatform.gameObject, false))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsInitialized()
        {
            return this.Platform;
        }
    }
}
