namespace Subnautica.Server.Abstracts.Processors
{
    using Subnautica.Network.Models.Server;
    using Subnautica.Network.Models.Storage.Construction;
    using Subnautica.Server.Core;

    public abstract class MetadataProcessor : BaseProcessor
    {
        public abstract bool OnDataReceived(AuthorizationProfile profile, MetadataComponentArgs networkPacket, ConstructionItem construction);

        public static bool ExecuteProcessor(AuthorizationProfile profile, MetadataComponentArgs networkPacket, ConstructionItem construction, TechType processType = TechType.None)
        {
            if (ProcessorShared.MetadataProcessors.TryGetValue(processType == TechType.None ? construction.TechType : processType, out var processor))
            {
                processor.OnDataReceived(profile, networkPacket, construction);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RedirectProcessor(AuthorizationProfile profile, MetadataComponentArgs networkPacket, ConstructionItem construction, TechType processType = TechType.None)
        {
            return ExecuteProcessor(profile, networkPacket, construction, processType);
        }
    }
}
