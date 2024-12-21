namespace Subnautica.Client.Abstracts.Processors
{
    using Subnautica.API.Features;
    using Subnautica.Network.Core.Components;

    public abstract class WorldEntityProcessor : BaseProcessor
    {
        public abstract bool OnDataReceived(NetworkWorldEntityComponent packet, byte requesterId, bool isSpawning);

        public static bool ExecuteProcessor(NetworkWorldEntityComponent packet, byte requesterId, bool isSpawning)
        {
            if (ProcessorShared.WorldEntityProcessors.TryGetValue(packet.ProcessType, out var processor))
            {
                processor.OnDataReceived(packet, requesterId, isSpawning);
                return true;
            }
            else
            {
                Log.Error(string.Format("WorldEntityProcessor Not Found: {0}, UniqueId: {1}", packet.ProcessType, packet.UniqueId));
                return false;
            }
        }
    }
}