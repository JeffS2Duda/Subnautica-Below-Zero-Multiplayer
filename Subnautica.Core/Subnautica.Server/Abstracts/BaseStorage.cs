namespace Subnautica.Server.Abstracts
{
    using System;
    using System.IO;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Core;

    public abstract class BaseStorage
    {   
        public object ProcessLock { get; set; } = new object();

        public string ServerId { get; set; }

        public string FilePath { get; set; }

        public abstract void Load();

        public abstract void Start(string serverId);

        public abstract void SaveToDisk();

        public bool WriteToDisk<T>(T storage)
        {
            if (storage == null)
            {
                Log.Error(string.Format("Storage.WriteToDisk -> Error Code (0x01): {0}", this.FilePath));
                return false;
            }

            var data = NetworkTools.Serialize(storage);
            if (data == null)
            {
                Log.Error(string.Format("Storage.WriteToDisk -> Error Code (0x02): {0}", this.FilePath));
                return false;
            }

            if (!data.IsValid())
            {
                Log.Error(string.Format("Storage.WriteToDisk -> Error Code (0x03): {0}", this.FilePath));
                return false;
            }

            return data.WriteToDisk(this.FilePath);
        }

        public bool InitializePath()
        {
            if (this.FilePath.IsNull())
            {
                return false;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(this.FilePath));
            return true;
        }
    }
}
