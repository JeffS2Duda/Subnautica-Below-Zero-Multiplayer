namespace Subnautica.Client.Synchronizations.Processors.WorldEntities
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class ThermalLilyProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            return true;
        }

        public static void OnThermalLilyRangeChecking(ThermalLilyRangeCheckingEventArgs ev)
        {
            ev.IsAllowed = false;

            double range = ThermalLilyProcessor.GetPlayerRange(ev.PlayerRange);
            double distance = ThermalLilyProcessor.GetPlayerDistanceFromLily(Utils.GetLocalPlayerPos(), ev.LilyPosition);

            if (distance < range)
            {
                ev.IsPlayerInRange = true;
            }
            else
            {
                foreach (var player in ZeroPlayer.GetPlayers())
                {
                    if (ThermalLilyProcessor.GetPlayerDistanceFromLily(player.Position, ev.LilyPosition) < range)
                    {
                        ev.IsPlayerInRange = true;
                        break;
                    }
                }
            }
        }

        public static void OnThermalLilyAnimationAnglesChecking(ThermalLilyAnimationAnglesCheckingEventArgs ev)
        {
            ev.IsAllowed = false;

            var distances = new Dictionary<byte, double>();
            var range = ThermalLilyProcessor.GetPlayerRange(ev.PlayerRange);
            var distance = ThermalLilyProcessor.GetPlayerDistanceFromLily(Utils.GetLocalPlayerPos(), ev.LilyPosition);

            if (distance < range)
            {
                distances.Add(0, distance);
            }

            foreach (var player in ZeroPlayer.GetPlayers())
            {
                distance = ThermalLilyProcessor.GetPlayerDistanceFromLily(player.Position, ev.LilyPosition);

                if (distance < range)
                {
                    distances.Add(player.PlayerId, distance);
                }
            }

            if (distances.Count > 0)
            {
                var player = ZeroPlayer.GetPlayerById(distances.OrderBy(q => q.Value).FirstOrDefault().Key);
                if (player == null)
                {
                    ev.PlayerPosition = Utils.GetLocalPlayerPos();
                }
                else
                {
                    ev.PlayerPosition = player.Position;
                }
            }
            else
            {
                ev.PlayerPosition = Utils.GetLocalPlayerPos();
            }
        }


        private static double GetPlayerRange(float range)
        {
            return (double)range * (double)range;
        }

        private static double GetPlayerDistanceFromLily(Vector3 playerPosition, Vector3 lilyPosition)
        {
            return (double)(playerPosition - lilyPosition).sqrMagnitude;
        }
    }
}

