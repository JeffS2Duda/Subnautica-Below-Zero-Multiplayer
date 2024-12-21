namespace Subnautica.Client.Multiplayer.Furnitures
{
    using System.Collections.Generic;
    using System.Linq;

    using Subnautica.API.Extensions;

    using Metadata = Subnautica.Network.Models.Metadata;

    public class Bed
    {
        private static HashSet<string> Beds { get; set; } = new HashSet<string>();

        public static int GetSleepingPlayerCount()
        {
            return Beds.Count;
        }

        public static bool UpdateBed(string playerId)
        {
            return Bed.Beds.Add(playerId);
        }

        public static bool IsSleeping(string playerId)
        {
            return Bed.Beds.Contains(playerId);
        }

        public static bool ClearBed(string playerId)
        {
            return Bed.Beds.Remove(playerId);
        }

        public static void Dispose()
        {
            Beds.Clear();
        }
    }
}