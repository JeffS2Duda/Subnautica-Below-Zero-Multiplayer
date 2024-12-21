namespace Subnautica.API.Features
{
    using Subnautica.API.Features.Creatures.Datas;

    using System.Collections.Generic;

    public class CreatureData
    {
        private static CreatureData instance;

        public static CreatureData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CreatureData();
                }

                return instance;
            }
        }

        private Dictionary<TechType, BaseCreatureData> Datas { get; set; } = new Dictionary<TechType, BaseCreatureData>();

        public CreatureData()
        {
            this.Register(new SkyrayData());
            this.Register(new CrashFishData());
            this.Register(new TitanHolefishData());
            this.Register(new JellyFishData());
            this.Register(new ArcticRayData());
            this.Register(new VentGardenSmallData());
            this.Register(new LilyPaddlerData());
            this.Register(new GlowWhaleData());
            this.Register(new ChelicerateData());
            this.Register(new ShadowLeviathanData());
            this.Register(new VoidLeviathanData());
            this.Register(new BruteSharkData());
            this.Register(new CryptosuchusData());

            this.Register(new GlowWhaleEggData());
        }

        public bool IsExists(TechType type)
        {
            return this.Datas.ContainsKey(type);
        }

        public BaseCreatureData GetCreatureData(TechType type)
        {
            this.Datas.TryGetValue(type, out BaseCreatureData creatureData);
            return creatureData;
        }

        public void Register(BaseCreatureData creatureData)
        {
            if (this.Datas.ContainsKey(creatureData.CreatureType))
            {
                Log.Error($"Creature Register Error: It has already been defined - {creatureData.CreatureType}");
            }
            else
            {
                this.Datas.Add(creatureData.CreatureType, creatureData);
            }
        }

        public void UnRegister(TechType techType)
        {
            this.Datas.Remove(techType);
        }
    }
}
