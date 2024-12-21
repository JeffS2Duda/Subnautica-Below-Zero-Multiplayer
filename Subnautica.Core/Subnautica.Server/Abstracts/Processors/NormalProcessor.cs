namespace Subnautica.Server.Abstracts.Processors
{
    using Subnautica.Network.Models.Core;

    using Subnautica.Server.Core;

    public abstract class NormalProcessor : BaseProcessor
    {
        public abstract bool OnExecute(AuthorizationProfile authorization, NetworkPacket networkPacket);

        public static bool ExecuteProcessor(AuthorizationProfile profile, NetworkPacket packet)
        {
            if (ProcessorShared.Processors.TryGetValue(packet.Type, out var processor))
            {
                processor.OnExecute(profile, packet);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
