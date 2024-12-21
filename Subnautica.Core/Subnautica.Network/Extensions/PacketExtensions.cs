namespace Subnautica.Network.Extensions
{
    using LiteNetLib;
    using LiteNetLib.Utils;

    using Subnautica.Network.Core;
    using Subnautica.Network.Models.Core;

    public static class PacketExtensions
    {
        public static byte[] Serialize(this NetworkPacket packet)
        {
            return NetworkTools.Serialize(packet);
        }

        public static NetworkPacket GetPacket(this NetPacketReader reader)
        {
            return NetworkTools.Deserialize<NetworkPacket>(reader.GetRemainingBytesSegment());
        }

        public static NetworkPacket GetPacket(this NetDataReader reader)
        {
            return NetworkTools.Deserialize<NetworkPacket>(reader.GetRemainingBytesSegment());
        }
    }
}
