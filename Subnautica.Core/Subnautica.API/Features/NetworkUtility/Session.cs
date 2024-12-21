namespace Subnautica.API.Features.NetworkUtility
{
    using System.Linq;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Client;
    using Subnautica.Network.Models.Storage.World.Childrens;

    public class Session
    {
        public JoiningServerArgs Current { get; private set; }

        public double EndGameWorldTime { get; private set; }

        public bool IsInSeaTruck { get; set; }

        public void SetSession(JoiningServerArgs session)
        {
            this.Current = session;
        }

        public void AddDiscoveredTechType(TechType techType)
        {
            this.Current.DiscoveredTechTypes.Add(techType);
        }

        public Brinicle GetBrinicle(string uniqueId)
        {
            return this.Current.Brinicles.FirstOrDefault(q => q.UniqueId == uniqueId);
        }

        public void SetBrinicle(Brinicle brinicle)
        {
            this.Current.Brinicles.RemoveWhere(q => q.UniqueId == brinicle.UniqueId);
            this.Current.Brinicles.Add(brinicle);
        }

        public bool IsBrinicleExists(string uniqueId)
        {
            return this.Current.Brinicles.Any(q => q.UniqueId == uniqueId);
        }

        public bool IsCosmeticItemExists(string uniqueId)
        {
            return this.Current.CosmeticItems.Any(q => q.StorageItem.ItemId == uniqueId);
        }

        public void SetCosmeticItem(CosmeticItem cosmeticItem)
        {
            this.Current.CosmeticItems.RemoveWhere(q => q.StorageItem.ItemId == cosmeticItem.StorageItem.ItemId);
            this.Current.CosmeticItems.Add(cosmeticItem);
        }

        public void RemoveCosmeticItem(string uniqueId)
        {
            this.Current.CosmeticItems.RemoveWhere(q => q.StorageItem.ItemId == uniqueId);
        }

        public bool SetConstructionComponent(string uniqueId, MetadataComponent component)
        {
            var construction = this.Current.Constructions.FirstOrDefault(q => q.UniqueId == uniqueId);
            if (construction == null)
            {
                return false;
            }

            construction.Component = component;
            return true;
        }

        public double GetWorldTime()
        {
            if (BelowZeroEndGame.isActive)
            {
                return this.EndGameWorldTime;
            }

            return DayNightCycle.main.timePassedAsDouble;
        }

        public void SetEndGameWorldTime(double time, bool isAdd = false)
        {
            if (isAdd)
            {
                this.EndGameWorldTime += time;
            }
            else
            {
                this.EndGameWorldTime = time;
            }
        }

        public void Dispose()
        {
            this.Current      = null;
            this.IsInSeaTruck = false;
        }
    }
}
