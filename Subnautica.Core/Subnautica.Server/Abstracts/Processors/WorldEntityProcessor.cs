namespace Subnautica.Server.Abstracts.Processors
{
    using Subnautica.API.Features;
    using Subnautica.Network.Models.Server;
    using Subnautica.Server.Abstracts;
    using Subnautica.Server.Core;

    public abstract class WorldEntityProcessor : BaseProcessor
    {
        public abstract bool OnDataReceived(AuthorizationProfile profile, WorldEntityActionArgs packet);

        public static bool ExecuteProcessor(AuthorizationProfile profile, WorldEntityActionArgs packet)
        {
            if (ProcessorShared.WorldEntityProcessors.TryGetValue(packet.Entity.ProcessType, out var processor))
            {
                processor.OnDataReceived(profile, packet);
                return true;
            }
            else
            {
                Log.Error(string.Format("WorldEntityProcessor Not Found: {0}, UniqueId: {1}", packet.Entity.ProcessType, packet.Entity.UniqueId));
                return false;
            }
        }
    }
}
