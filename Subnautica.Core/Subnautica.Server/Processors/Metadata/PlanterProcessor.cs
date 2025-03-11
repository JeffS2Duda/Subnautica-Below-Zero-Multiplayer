namespace Subnautica.Server.Processors.Metadata
{
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Metadata;
    using Subnautica.Network.Models.Server;
    using Subnautica.Network.Models.Storage.Construction;
    using Subnautica.Server.Abstracts.Processors;
    using Subnautica.Server.Core;
    using System;
    using System.Linq;
    using Metadata = Subnautica.Network.Models.Metadata;

    public class PlanterProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(AuthorizationProfile profile, MetadataComponentArgs packet, ConstructionItem construction)
        {
            if (Server.Instance.Logices.Interact.IsBlocked(construction.UniqueId, profile.UniqueId))
            {
                return false;
            }

            var component = packet.Component.GetComponent<Metadata.Planter>();
            if (component == null || component.CurrentItem == null)
            {
                return false;
            }

            Subnautica.Network.Models.Metadata.Planter planter = this.GetPlanter(component, construction);
            if (planter == null)
                return false;
            if (packet.TechType == TechType.BaseWaterPark)
                packet.TechType = TechType.FarmingTray;
            if (component.IsAdding)
            {
                if (planter.Items.Any<PlanterItem>((Func<PlanterItem, bool>)(q => (int)q.SlotId == (int)component.CurrentItem.SlotId || q.ItemId == component.CurrentItem.ItemId)))
                    return false;
                component.CurrentItem.TimeStartGrowth = Subnautica.Server.Core.Server.Instance.Logices.World.GetServerTime();
                component.CurrentItem.TimeNextFruit = component.CurrentItem.TimeStartGrowth + (float)component.CurrentItem.Duration;
                planter.Items.Add(component.CurrentItem);
                profile.SendPacketToAllClient((NetworkPacket)packet);
            }
            else if (component.IsHarvesting)
            {
                PlanterItem planterItem = planter.Items.FirstOrDefault<PlanterItem>((Func<PlanterItem, bool>)(q => q.ItemId == component.CurrentItem.ItemId));
                if (planterItem == null)
                    return false;
                component.CurrentItem.Health = planterItem.Health;
                if ((double)component.CurrentItem.FruitSpawnInterval == -1.0)
                {
                    planter.Items.Remove(planterItem);
                    profile.SendPacketToAllClient((NetworkPacket)packet);
                }
                else
                {
                    planterItem.MaxSpawnableFruit = component.CurrentItem.MaxSpawnableFruit;
                    planterItem.FruitSpawnInterval = component.CurrentItem.FruitSpawnInterval;
                    planterItem.SyncFruits(Subnautica.Server.Core.Server.Instance.Logices.World.GetServerTime());
                    if (planterItem.ActiveFruitCount > (byte)0)
                    {
                        --planterItem.ActiveFruitCount;
                        component.CurrentItem = planterItem;
                        profile.SendPacketToAllClient((NetworkPacket)packet);
                    }
                }
            }
            else if ((double)component.CurrentItem.Health != -1.0)
            {
                PlanterItem planterItem = planter.Items.FirstOrDefault<PlanterItem>((Func<PlanterItem, bool>)(q => q.ItemId == component.CurrentItem.ItemId));
                if (planterItem == null)
                    return false;
                planterItem.Health = component.CurrentItem.Health;
                component.CurrentItem.SlotId = planterItem.SlotId;
                if ((double)planterItem.Health <= 0.0)
                    planter.Items.Remove(planterItem);
                profile.SendPacketToOtherClients((NetworkPacket)packet);
            }
            return true;
        }

        private Subnautica.Network.Models.Metadata.Planter GetPlanter(
          Subnautica.Network.Models.Metadata.Planter component,
          ConstructionItem construction)
        {
            if (construction.TechType != TechType.BaseWaterPark)
                return construction.EnsureComponent<Subnautica.Network.Models.Metadata.Planter>();
            BaseWaterPark baseWaterPark = construction.EnsureComponent<BaseWaterPark>();
            return component.IsLeft ? baseWaterPark.LeftPlanter : baseWaterPark.RightPlanter;
        }
    }
}


