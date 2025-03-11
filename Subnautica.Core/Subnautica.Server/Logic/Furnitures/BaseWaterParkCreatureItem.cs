namespace Subnautica.Server.Logic.Furnitures;

using System;
using Subnautica.Network.Models.Metadata;
using Subnautica.Network.Models.Storage.Construction;
using Subnautica.Network.Models.Storage.World.Childrens;
using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;
using Subnautica.Server.Core;

public class BaseWaterParkCreatureItem
{
    public string WaterParkId { get; set; }

    public string EntityId { get; set; }

    public bool IsDestroyEgg { get; set; }

    public BaseWaterParkCreatureItem(string waterParkId, string entityId, bool destroyEgg = false)
    {
        this.WaterParkId = waterParkId;
        this.EntityId = entityId;
        this.IsDestroyEgg = destroyEgg;
    }

    public WaterParkCreature GetEntity()
    {
        ConstructionItem construction = Server.Instance.Storages.Construction.GetConstruction(this.WaterParkId);
        WaterParkCreature waterParkCreature;
        if (construction == null || construction.TechType != TechType.BaseWaterPark)
        {
            waterParkCreature = null;
        }
        else
        {
            Subnautica.Network.Models.Metadata.BaseWaterPark baseWaterPark = construction.EnsureComponent<Subnautica.Network.Models.Metadata.BaseWaterPark>();
            if (baseWaterPark == null)
            {
                waterParkCreature = null;
            }
            else
            {
                WorldDynamicEntity creature = baseWaterPark.GetCreature(this.EntityId);
                if (creature == null)
                {
                    waterParkCreature = null;
                }
                else
                {
                    waterParkCreature = creature.Component.GetComponent<WaterParkCreature>();
                }
            }
        }
        return waterParkCreature;
    }
}