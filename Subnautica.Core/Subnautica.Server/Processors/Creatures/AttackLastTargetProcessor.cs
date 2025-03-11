namespace Subnautica.Server.Processors.Creatures
{
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Creatures;
    using Subnautica.Network.Models.Server;
    using Subnautica.Server.Abstracts.Processors;
    using Subnautica.Server.Core;
    using Subnautica.Server.Extensions;

    public class AttackLastTargetProcessor : NormalProcessor
    {
        public override bool OnExecute(AuthorizationProfile profile, NetworkPacket networkPacket)
        {
            CreatureAttackLastTargetArgs packet = networkPacket.GetPacket<CreatureAttackLastTargetArgs>();
            if (packet == null)
                return this.SendEmptyPacketErrorLog(networkPacket);
            MultiplayerCreatureItem creature;
            if (!Server.Instance.Logices.CreatureWatcher.TryGetCreature(packet.CreatureId, out creature) || creature.LiveMixin.IsDead || (int)creature.OwnerId != (int)profile.PlayerId || creature.TechType == TechType.GlowWhale)
                return false;
            if (packet.IsStopped)
            {
                if (creature.GetActionType() == ProcessType.CreatureAttackLastTarget)
                    Server.Instance.Logices.CreatureWatcher.ClearAction(creature);
                profile.SendPacketToAllClient(packet);
            }
            else
            {
                byte targetOwnerId = packet.Target.GetTargetOwnerId(profile.PlayerId);
                if (targetOwnerId <= (byte)0)
                    return false;
                if (packet.Target.IsPlayer())
                {
                    AuthorizationProfile player = Server.Instance.GetPlayer(targetOwnerId);
                    if (player == null || !player.IsFullConnected || player.IsUnderAttack())
                        return false;
                }
                creature.SetAction(packet, targetOwnerId);
                if (Server.Instance.Logices.CreatureWatcher.ImmediatelyTrigger())
                {
                    Server.Instance.Logices.CreatureWatcher.TriggerAction(packet);
                    Server.Instance.Logices.CreatureWatcher.ClearAction(creature, 5f);
                }
            }
            return true;
        }
    }
}
