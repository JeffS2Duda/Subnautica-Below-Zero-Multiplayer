namespace Subnautica.Network.Core
{
    using System;

    using MessagePack;

    public class NetworkTools
    {
        private static MessagePackSerializerOptions mainCompression;

        public static MessagePackSerializerOptions Lz4Compression 
        { 
            get
            {
                if (mainCompression == null)
                {
                    mainCompression = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
                }

                return mainCompression;
            }
        }

        public static byte[] Serialize<T>(T data)
        {
            return MessagePackSerializer.Serialize(data, Lz4Compression);
        }

        public static T Deserialize<T>(ArraySegment<byte> data)
        {
            return MessagePackSerializer.Deserialize<T>(data, Lz4Compression);
        }

        public static T Deserialize<T>(byte[] data)
        {
            return MessagePackSerializer.Deserialize<T>(data, Lz4Compression);
        }
    }
}
