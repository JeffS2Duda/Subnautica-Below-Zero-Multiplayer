namespace Subnautica.Server.Abstracts
{
    using Subnautica.API.Features;

    using Subnautica.Network.Models.Core;

    public abstract class BaseProcessor
    {
        public virtual void OnStart()
        {
        }

        public bool SendEmptyPacketErrorLog(NetworkPacket networkPacket)
        {
            Log.Error(string.Format("Packet Is Null, Type: {0}, Channel Type: {1}", networkPacket.Type, networkPacket.ChannelType));
            return false;
        }

        public bool SendErrorLog(string message)
        {
            Log.Error(message);
            return false;
        }
    }
}
