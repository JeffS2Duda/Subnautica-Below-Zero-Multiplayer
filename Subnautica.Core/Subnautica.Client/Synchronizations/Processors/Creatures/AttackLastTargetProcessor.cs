namespace Subnautica.Client.Synchronizations.Processors.Creatures
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Creatures;
    using Subnautica.API.Features.Creatures.MonoBehaviours.Shared;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;

    using ServerModel = Subnautica.Network.Models.Server;

    public class AttackLastTargetProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.CreatureAttackLastTargetArgs>();
            if (packet == null)
            {
                return false;
            }

            var action = new CreatureQueueAction();
            action.OnProcessCompleted = this.OnCreatureProcessCompleted;
            action.RegisterProperty("Target", packet.Target);
            action.RegisterProperty("IsStopped", packet.IsStopped);

            Network.Creatures.ProcessToQueue(packet.CreatureId, action);
            return true;
        }

        private void OnCreatureProcessCompleted(MultiplayerCreature creature, CreatureQueueItem item)
        {
            if (creature.GameObject.TryGetComponent<MultiplayerAttackLastTarget>(out var attackLastTarget))
            {
                if (item.Action.GetProperty<bool>("IsStopped"))
                {
                    attackLastTarget.StopAttackSoundAndAnimation(true);
                }
                else
                {
                    attackLastTarget.ForceAttackTarget(item.Action.GetProperty<ZeroLastTarget>("Target").GetGameObject(true));
                }
            }
        }

        public static void OnCreatureAttackLastTargetStarting(CreatureAttackLastTargetStartingEventArgs ev)
        {
            if (ev.Creature.gameObject.GetTechType() != TechType.GlowWhale)
            {
                ev.IsAllowed = false;

                AttackLastTargetProcessor.SendPacketToServer(ev.UniqueId.ToCreatureId(), ev.Target.GetIdentityId(), ev.Target.GetTechType());
            }
        }

        public static void OnCreatureAttackLastTargetStopped(CreatureAttackLastTargetStoppedEventArgs ev)
        {
            if (ev.IsAttackAnimationActive && ev.Creature.gameObject.GetTechType() != TechType.GlowWhale)
            {
                AttackLastTargetProcessor.SendPacketToServer(ev.UniqueId.ToCreatureId(), isStopped: true);
            }
        }

        private static void SendPacketToServer(ushort creatureId, string targetId = null, TechType techType = TechType.None, bool isStopped = false)
        {
            ServerModel.CreatureAttackLastTargetArgs request = new ServerModel.CreatureAttackLastTargetArgs()
            {
                CreatureId = creatureId,
                IsStopped = isStopped,
                Target = new ZeroLastTarget(targetId, techType)
            };

            NetworkClient.SendPacket(request);
        }
    }
}
