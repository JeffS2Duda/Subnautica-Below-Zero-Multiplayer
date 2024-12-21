namespace Subnautica.Client.Abstracts
{
    using Subnautica.Network.Models.Core;

    public abstract class NormalProcessor : BaseProcessor
    {

        public abstract bool OnDataReceived(NetworkPacket networkPacket);
    }
}
