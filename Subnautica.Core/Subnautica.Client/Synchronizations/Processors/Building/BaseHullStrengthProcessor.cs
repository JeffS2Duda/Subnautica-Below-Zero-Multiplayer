namespace Subnautica.Client.Synchronizations.Processors.Building
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;
    using System.Collections.Generic;
    using System.Linq;
    using ServerModel = Subnautica.Network.Models.Server;

    public class BaseHullStrengthProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.BaseHullStrengthTakeDamagingArgs>();
            if (packet.UniqueId.IsNull())
            {
                return false;
            }

            var gameObject = Network.Identifier.GetGameObject(packet.UniqueId);
            if (gameObject == null)
            {
                return false;
            }

            var liveMixin = gameObject.GetComponentInParent<global::LiveMixin>();
            if (liveMixin == null)
            {
                return false;
            }

            if (packet.CurrentHealth != -1f)
            {
                ZeroLiveMixin.TakeDamage(liveMixin, packet.CurrentHealth, packet.DamageType);
            }

            if (packet.LeakPoints != null)
            {
                var leakable = liveMixin.GetComponentInParent<global::Leakable>();
                if (leakable)
                {
                    SyncLeaks(leakable, packet.LeakPoints);
                }
            }

            return true;
        }

        public static void SyncLeaks(global::Leakable leakable, List<ZeroVector3> points)
        {
            foreach (var item in leakable.leakingLeakPoints.ToList())
            {
                if (!points.Contains(item.transform.position.ToZeroVector3()))
                {
                    item.pointActive = false;

                    leakable.leakingLeakPoints.Remove(item);
                    leakable.unusedLeakPoints.Add(item);
                }
            }

            foreach (var item in leakable.unusedLeakPoints.ToList())
            {
                if (points.Contains(item.transform.position.ToZeroVector3()))
                {
                    item.pointActive = true;

                    leakable.unusedLeakPoints.Remove(item);
                    leakable.leakingLeakPoints.Add(item);
                }
            }
        }

        public static void OnTakeDamaging(TakeDamagingEventArgs ev)
        {
            if (!ev.IsStaticWorldEntity && !ev.IsDestroyable && ev.OldHealth > 0f)
            {
                if (ev.LiveMixin.TryGetComponent<global::Leakable>(out var component))
                {
                    ev.IsAllowed = false;

                    if (ev.Damage > 0)
                    {
                        BaseHullStrengthProcessor.SendPacketToServer(Network.Identifier.GetIdentityId(ev.LiveMixin.GetComponentInChildren<BaseDeconstructable>().gameObject, false), ev.Damage, ev.DamageType, ev.LiveMixin.maxHealth, GetLeakPoints(component));
                    }
                }
            }
        }

        public static void OnBaseHullStrengthCrushing(BaseHullStrengthCrushingEventArgs ev)
        {
            ev.IsAllowed = false;
        }

        private static void SendPacketToServer(string uniqueId, float damage = -1, DamageType damageType = DamageType.Normal, float maxHealth = -1, List<ZeroVector3> leakPoints = default)
        {
            ServerModel.BaseHullStrengthTakeDamagingArgs request = new ServerModel.BaseHullStrengthTakeDamagingArgs()
            {
                UniqueId = uniqueId,
                Damage = damage,
                DamageType = damageType,
                MaxHealth = maxHealth,
                LeakPoints = leakPoints,
            };

            NetworkClient.SendPacket(request);
        }

        private static List<ZeroVector3> GetLeakPoints(global::Leakable component)
        {
            var leakPoints = new List<ZeroVector3>();
            foreach (var point in component.unusedLeakPoints)
            {
                leakPoints.Add(point.transform.position.ToZeroVector3());
            }

            foreach (var point in component.leakingLeakPoints)
            {
                leakPoints.Add(point.transform.position.ToZeroVector3());
            }

            return leakPoints;
        }
    }
}
