namespace Subnautica.Server.Abstracts.Processors
{
    using Subnautica.Network.Models.Server;
    using Subnautica.Server.Core;

    public abstract class PlayerItemProcessor : BaseProcessor
    {
        public abstract bool OnDataReceived(AuthorizationProfile profile, PlayerItemActionArgs packet);

        public static bool ExecuteProcessor(AuthorizationProfile profile, PlayerItemActionArgs packet)
        {
            if (ProcessorShared.PlayerItemProcessors.TryGetValue(packet.Item.TechType, out var processor))
            {
                processor.OnDataReceived(profile, packet);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
