using MessagePack;
using Subnautica.Network.Core.Components;
using Subnautica.Network.Models.Storage.World.Childrens;
using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnautica.Network.Models.Metadata;

[MessagePackObject(false)]
public class BaseWaterPark : MetadataComponent
{
    [Key(0)]
    public List<string> Eggs { get; set; } = new List<string>();

    [Key(1)]
    public List<WorldDynamicEntity> Creatures { get; set; } = new List<WorldDynamicEntity>();

    [Key(2)]
    public Planter LeftPlanter { get; set; } = new Planter
    {
        IsLeft = true
    };

    [Key(3)]
    public Planter RightPlanter { get; set; } = new Planter();

    [Key(4)]
    public BaseWaterParkProcessType ProcessType { get; set; }

    [Key(5)]
    public WorldDynamicEntity Entity { get; set; }

    [Key(6)]
    public WorldPickupItem WorldPickupItem { get; set; }

    [Key(7)]
    public Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.WaterParkCreature WaterParkCreature { get; set; }

    public WorldDynamicEntity GetCreature(string entityId)
    {
        return this.Creatures.FirstOrDefault((WorldDynamicEntity q) => q.UniqueId == entityId);
    }

    public List<string> GetEggs()
    {
        return this.Eggs.ToList<string>();
    }

    public List<WorldDynamicEntity> GetCreatures()
    {
        return this.Creatures.ToList<WorldDynamicEntity>();
    }

    public bool AddCreature(WorldDynamicEntity entity, bool force = false)
    {
        bool flag = !force && this.Creatures.Any((WorldDynamicEntity q) => q.UniqueId == entity.UniqueId);
        bool flag2;
        if (flag)
        {
            flag2 = false;
        }
        else
        {
            this.Creatures.Add(entity);
            flag2 = true;
        }
        return flag2;
    }

    public bool IsExistsCreatureEgg(string entityId)
    {
        return this.Eggs.Contains(entityId);
    }

    public bool RemoveCreature(string entityId)
    {
        WorldDynamicEntity creature = this.GetCreature(entityId);
        bool flag = creature != null;
        return flag && this.Creatures.Remove(creature);
    }

    public bool AddCreatureEgg(string uniqueId, bool force = false)
    {
        bool flag = !force && this.IsFull();
        bool flag2;
        if (flag)
        {
            flag2 = false;
        }
        else
        {
            bool flag3 = this.Eggs.Any((string q) => q == uniqueId);
            if (flag3)
            {
                flag2 = false;
            }
            else
            {
                this.Eggs.Add(uniqueId);
                flag2 = true;
            }
        }
        return flag2;
    }

    public bool RemoveCreatureEgg(string uniqueId)
    {
        return this.Eggs.Remove(uniqueId);
    }

    public bool IsFull()
    {
        return this.Creatures.Count + this.Eggs.Count >= this.GetLimit();
    }

    public int GetLimit()
    {
        return 20;
    }
}