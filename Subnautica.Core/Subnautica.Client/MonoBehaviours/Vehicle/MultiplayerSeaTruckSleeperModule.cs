namespace Subnautica.Client.MonoBehaviours.Vehicle
{
    using Subnautica.API.Features;
    using Subnautica.Client.MonoBehaviours.Player;

    using UnityEngine;

    public class MultiplayerSeaTruckSleeperModule : MonoBehaviour
    {
        private global::Bed Bed;

        public void Start()
        {
            this.Bed = this.GetComponentInChildren<global::Bed>(true);
        }

        public void OnMultiplayerPlayerDisconnected(ZeroPlayer player)
        {
            if (this.IsSamePlayer(this.GetPlayer(), player))
            {
                this.Bed.animator.Rebind();
            }
        }

        public void OnDestroy()
        {
            var player = this.GetPlayer();
            if (player != null)
            {
                Multiplayer.Furnitures.Bed.ClearBed(player.UniqueId);

                player.SetParent(null);
                player.Animator.Rebind();
            }
        }

        private bool IsSamePlayer(ZeroPlayer player1, ZeroPlayer player2)
        {
            return player1 != null && player2 != null && player1.UniqueId == player2.UniqueId;
        }

        private ZeroPlayer GetPlayer()
        {
            return this.Bed.GetComponentInChildren<PlayerAnimation>()?.Player;
        }
    }
}
