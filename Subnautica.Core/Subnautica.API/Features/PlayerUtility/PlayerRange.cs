namespace Subnautica.API.Features.PlayerUtility
{
    using System.Collections.Generic;
    using System.Linq;

    public class PlayerRange
    {
        public ZeroPlayer NearestPlayer { get; private set; }

        public float NearestPlayerDistance { get; private set; } = 99999f;

        public ZeroPlayer FarthestPlayer { get; private set; }

        public float FarthestPlayerDistance { get; private set; } = -99999f;

        public ZeroPlayer RandomPlayer
        {
            get
            {
                if (this.randomPlayer == null)
                {
                    this.randomPlayer = ZeroPlayer.GetPlayerById(this.Players.ElementAt(Tools.GetRandomInt(0, this.Players.Count - 1)));
                }

                return this.randomPlayer;
            }
        }

        private ZeroPlayer randomPlayer;

        private List<byte> Players { get; set; } = new List<byte>();

        public void SetNearestPlayer(ZeroPlayer player, float distance)
        {
            this.NearestPlayer = player;
            this.NearestPlayerDistance = distance;
        }

        public void SetFarthestPlayer(ZeroPlayer player, float distance)
        {
            this.FarthestPlayer = player;
            this.FarthestPlayerDistance = distance;
        }

        public void AddPlayer(ZeroPlayer player, float distance)
        {
            this.Players.Add(player.PlayerId);
        }

        public bool IsExistsPlayer()
        {
            return this.Players.Count > 0;
        }
    }
}
