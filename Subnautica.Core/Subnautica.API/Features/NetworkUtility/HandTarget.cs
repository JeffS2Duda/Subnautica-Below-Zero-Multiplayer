namespace Subnautica.API.Features.NetworkUtility
{
    using System.Collections.Generic;

    using Subnautica.API.Extensions;

    public class HandTarget
    {
        public Dictionary<string, float> Targets = new Dictionary<string, float>();

        private float Delay { get; set; } = 0.5f;

        public bool IsBlocked(string uniqueId)
        {
            if (uniqueId.IsNull())
            {
                return false;
            }

            if (this.IsUsingFromCache(uniqueId))
            {
                return true;
            }

            if (Interact.IsBlocked(uniqueId))
            {
                this.AddTemporaryBlock(uniqueId);
                return true;
            }

            if (this.IsUsingFromCinematics(uniqueId))
            {
                this.AddTemporaryBlock(uniqueId);
                return true;
            }

            return false;
        }

        private bool IsUsingFromCinematics(string uniqueId)
        {
            foreach (var player in ZeroPlayer.GetPlayers())
            {
                if (player.IsCinematicModeActive && player.CurrentCinematicUniqueId == uniqueId)
                {
                    return true;
                }

                if (player.UsingRoomId == uniqueId)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsUsingFromCache(string uniqueId)
        {
            return this.Targets.TryGetValue(uniqueId, out var target) && Network.Session.GetWorldTime() < target;
        }
 
        public void AddTemporaryBlock(string uniqueId)
        {
            this.Targets[uniqueId] = (float)(Network.Session.GetWorldTime() + this.Delay);
        }

        public void Dispose()
        {
            this.Targets.Clear();
        }
    }
}
