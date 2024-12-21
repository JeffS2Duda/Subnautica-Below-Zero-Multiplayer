namespace Subnautica.API.Features.NetworkUtility
{
    using System.Collections.Generic;

    using UWE;

    public class EntityDatabase
    {
        private readonly Dictionary<TechType, WorldEntityInfo> TechTypeInfos = new Dictionary<TechType, WorldEntityInfo>();

        public void AddTechTypeInfo(TechType techType, WorldEntityInfo info)
        {
            this.TechTypeInfos[techType] = info;
        }

        public bool TryGetInfoByTechType(TechType techType, out WorldEntityInfo info)
        {
            return this.TechTypeInfos.TryGetValue(techType, out info);
        }

        public bool TryGetInfoByClassId(string classId, out WorldEntityInfo info)
        {
            return WorldEntityDatabase.TryGetInfo(classId, out info);
        }

        public void Dispose()
        {

        }
    }
}
