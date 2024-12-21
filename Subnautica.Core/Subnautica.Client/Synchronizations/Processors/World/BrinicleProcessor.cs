namespace Subnautica.Client.Synchronizations.Processors.World
{
    using System.Collections.Generic;
    using System.Linq;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Client.MonoBehaviours.World;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.World.Childrens;

    using UnityEngine;

    using ServerModel = Subnautica.Network.Models.Server;

    public class BrinicleProcessor : NormalProcessor
    {
        private static readonly HashSet<Brinicle> WaitingForSending = new HashSet<Brinicle>();

        private readonly StopwatchItem Timing = new StopwatchItem(1000f);

        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.BrinicleArgs>();
            
            if (packet.Brinicles.Count > 0)
            {
                foreach (var brinicle in packet.Brinicles)
                {
                    Network.Session.SetBrinicle(brinicle);
                }
            }

            return true;
        }

        public override void OnFixedUpdate()
        {
            if (World.IsLoaded && this.Timing.IsFinished())
            {
                this.Timing.Restart();

                if (WaitingForSending.Count > 0)
                {
                    BrinicleProcessor.SendPacketToServer(waitingForRegistry: WaitingForSending.ToList());

                    WaitingForSending.Clear();
                }
            }
        }

        public static void OnEntitySpawned(EntitySpawnedEventArgs ev)
        {
            if (ev.TechType == TechType.Brinicle)
            {
                if (!Network.Session.IsBrinicleExists(ev.UniqueId) && !WaitingForSending.Any(q => q.UniqueId == ev.UniqueId))
                {
                    WaitingForSending.Add(GetBrinicleItem(ev.GameObject, ev.UniqueId));
                }

                ev.GameObject.EnsureComponent<MultiplayerBrinicle>();
            }
        }

        public static void OnTakeDamaging(TakeDamagingEventArgs ev)
        {
            if (ev.TechType == TechType.Brinicle)
            {
                ev.IsAllowed = false;

                BrinicleProcessor.SendPacketToServer(uniqueId: ev.UniqueId, damage: ev.Damage);
            }
        }

        public static void SendPacketToServer(string uniqueId = null, List<Brinicle> waitingForRegistry = null, float damage = 0f)
        {
            ServerModel.BrinicleArgs request = new ServerModel.BrinicleArgs()
            {
                WaitingForRegistry = waitingForRegistry,
                UniqueId = uniqueId,
                Damage   = damage,
            };

            NetworkClient.SendPacket(request);
        }

        private static Brinicle GetBrinicleItem(GameObject gameObject, string uniqueId)
        {
            if (gameObject.TryGetComponent<global::Brinicle>(out var brinicle))
            {
                return new Brinicle()
                {
                    UniqueId = uniqueId,
                    MinFullScale = brinicle.minFullScale.ToZeroVector3(),
                    MaxFullScale = brinicle.maxFullScale.ToZeroVector3(),
                };
            }

            return null;
        }

        public override void OnDispose()
        {
            WaitingForSending.Clear();
        }
    }
}
