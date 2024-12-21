namespace Subnautica.Client.Synchronizations.Processors.World
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Client.Extensions;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using ServerModel = Subnautica.Network.Models.Server;

    public class PrecursorTeleporterProcessor : NormalProcessor
    {
        public static Dictionary<string, string> DisabledTeleporters = new Dictionary<string, string>();

        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.PrecursorTeleporterArgs>();
            if (packet == null)
            {
                return false;
            }

            if (packet.IsTerminal)
            {
                this.ActivateTargetTeleporter(packet.TeleporterId, packet.UniqueId);

                var player = ZeroPlayer.GetPlayerById(packet.GetPacketOwnerId());
                if (player == null)
                {
                    return false;
                }

                if (player.IsMine)
                {
                    player.OnHandClickPrecursorTerminal(packet.UniqueId);
                }
                else
                {
                    player.ActivatePrecursorTerminal(packet.UniqueId);
                }
            }
            else
            {
                var player = ZeroPlayer.GetPlayerById(packet.GetPacketOwnerId());
                if (player != null)
                {
                    if (packet.IsTeleportStart)
                    {
                        player.Hide(false);
                    }
                    else
                    {
                        player.Show(false);
                    }
                }
            }

            return true;
        }

        public void ActivateTargetTeleporter(string teleporterId, string uniqueId)
        {
            TeleporterManager.main.activeTeleporters.Add(teleporterId);
            TeleporterManager.main.activeTeleporters.Add(uniqueId);

            if (DisabledTeleporters.TryGetValue(teleporterId, out var targetTeleporterId))
            {
                DisabledTeleporters.Remove(teleporterId);

                UWE.CoroutineHost.StartCoroutine(this.ActivateTargetTeleporterAsync(targetTeleporterId));
            }
        }

        public IEnumerator ActivateTargetTeleporterAsync(string targetTeleporterId)
        {
            yield return new WaitForSecondsRealtime(4f);

            var teleporter = Network.Identifier.GetComponentByGameObject<global::PrecursorTeleporter>(targetTeleporterId, true);
            if (teleporter)
            {
                teleporter.ToggleDoor(true);
            }
        }

        public static void OnTeleporterInitialized(TeleporterInitializedEventArgs ev)
        {
            if (ev.IsExit)
            {
                if (TeleporterManager.GetTeleporterActive(ev.TeleporterId))
                {
                    DisabledTeleporters.Remove(ev.TeleporterId);
                }
                else
                {
                    DisabledTeleporters[ev.TeleporterId] = ev.UniqueId;
                }
            }
        }

        public static void OnPrecursorTeleporterUsed()
        {
            PrecursorTeleporterProcessor.SendPacketToServer(isTeleportStart: true);
        }

        public static void OnPrecursorTeleportationCompleted()
        {
            PrecursorTeleporterProcessor.SendPacketToServer(isTeleportCompleted: true);
        }

        public static void OnTeleporterTerminalActivating(TeleporterTerminalActivatingEventArgs ev)
        {
            ev.IsAllowed = false;

            if (!Interact.IsBlocked(ev.UniqueId))
            {
                PrecursorTeleporterProcessor.SendPacketToServer(ev.UniqueId, ev.TeleporterId, true);
            }
        }

        public static void SendPacketToServer(string uniqueId = null, string teleporterId = null, bool isTerminal = false, bool isTeleportStart = false, bool isTeleportCompleted = false)
        {
            ServerModel.PrecursorTeleporterArgs request = new ServerModel.PrecursorTeleporterArgs()
            {
                UniqueId = uniqueId,
                TeleporterId = teleporterId,
                IsTerminal = isTerminal,
                IsTeleportStart = isTeleportStart,
                IsTeleportCompleted = isTeleportCompleted,
            };

            NetworkClient.SendPacket(request);
        }

        public override void OnDispose()
        {
            DisabledTeleporters.Clear();
        }
    }
}
