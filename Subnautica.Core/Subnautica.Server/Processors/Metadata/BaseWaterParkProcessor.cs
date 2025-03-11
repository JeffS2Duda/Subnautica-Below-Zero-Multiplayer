namespace Subnautica.Server.Processors.Metadata
{
    using Subnautica.API.Extensions;
    using Subnautica.Network.Models.Metadata;
    using Subnautica.Network.Models.Server;
    using Subnautica.Network.Models.Storage.Construction;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Server.Abstracts.Processors;
    using Subnautica.Server.Core;

    public class BaseWaterParkProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(AuthorizationProfile profile, MetadataComponentArgs packet, ConstructionItem construction)
        {
            if (packet.Component is not BaseWaterPark)
                return base.RedirectProcessor(profile, packet, construction, TechType.PlanterPot);
            BaseWaterPark component = packet.Component.GetComponent<BaseWaterPark>();
            if (component == null)
            {
                return false;
            }

            if (component.WorldPickupItem.Item.TechType.IsCreatureEgg())
            {
                component.WorldPickupItem.Item.SetItem(component.WorldPickupItem.Item.TechType.ToCreatureEgg());
            }
            if (component.ProcessType == BaseWaterParkProcessType.ItemDrop)
            {
                BaseWaterPark baseWaterPark = construction.EnsureComponent<BaseWaterPark>();

                if (baseWaterPark.IsFull())
                {
                    component.ProcessType = BaseWaterParkProcessType.Full;
                    profile.SendPacketToAllClient(packet, false);
                    return false;
                }
                WorldDynamicEntity worldDynamicEntity;
                if (Server.Instance.Logices.BaseWaterPark.OnItemDrop(profile, baseWaterPark, component, construction.UniqueId, out worldDynamicEntity))
                {
                    component.Entity = worldDynamicEntity;
                    profile.SendPacketToAllClient(packet, false);
                }
            }
            else
            {
                if (component.ProcessType == BaseWaterParkProcessType.ItemPickup)
                {
                    if (Server.Instance.Logices.BaseWaterPark.OnItemPickup(profile, component))
                    {
                        profile.SendPacketToAllClient(packet, false);
                    }
                }
            }
            return true;
        }
    }
}
