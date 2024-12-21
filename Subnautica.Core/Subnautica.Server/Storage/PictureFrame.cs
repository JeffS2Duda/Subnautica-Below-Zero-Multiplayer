namespace Subnautica.Server.Storage
{
    using Subnautica.API.Features;
    using Subnautica.Network.Core;
    using Subnautica.Server.Abstracts;
    using System;
    using System.IO;
    using PictureFrameStorage = Network.Models.Storage.PictureFrame;

    public class PictureFrame : BaseStorage
    {
        public PictureFrameStorage.PictureFrame Storage { get; set; }

        public override void Start(string serverId)
        {
            this.ServerId = serverId;
            this.FilePath = Paths.GetMultiplayerServerSavePath(this.ServerId, "PictureFrame.bin");
            this.InitializePath();
            this.Load();
        }

        public override void Load()
        {
            if (File.Exists(this.FilePath))
            {
                try
                {
                    this.Storage = NetworkTools.Deserialize<PictureFrameStorage.PictureFrame>(File.ReadAllBytes(this.FilePath));
                }
                catch (Exception e)
                {
                    Log.Error($"PictureFrame.Load: {e}");
                }
            }
            else
            {
                this.Storage = new PictureFrameStorage.PictureFrame();
                this.SaveToDisk();
            }

            if (Core.Server.DEBUG)
            {
                Log.Info("PictureFrames: ");
                Log.Info("---------------------------------------------------------------");
                foreach (var item in this.Storage.Images)
                {
                    Log.Info(string.Format("ImageName: {0}, Size: {1}kb", item.Value.ImageName, Math.Round((double)item.Value.ImageData.Length / 1024.0, 0)));
                }
                Log.Info("---------------------------------------------------------------");
            }
        }

        public override void SaveToDisk()
        {
            lock (this.ProcessLock)
            {
                this.WriteToDisk(this.Storage);
            }
        }

        public Subnautica.Network.Models.Metadata.PictureFrame GetImage(string constructionUniqueId)
        {
            lock (this.ProcessLock)
            {
                if (this.Storage.Images.TryGetValue(constructionUniqueId, out var pictureFrame))
                {
                    return pictureFrame;
                }

                return null;
            }
        }

        public bool AddImage(string constructionUniqueId, string imageName, byte[] imageData)
        {
            lock (this.ProcessLock)
            {
                this.Storage.Images[constructionUniqueId] = new Subnautica.Network.Models.Metadata.PictureFrame(imageName, imageData, false);
                return true;
            }
        }

        public bool RemoveImage(string constructionUniqueId)
        {
            lock (this.ProcessLock)
            {
                if (this.Storage.Images.TryGetValue(constructionUniqueId, out var pictureFrame))
                {
                    return this.Storage.Images.Remove(constructionUniqueId);
                }

                return false;
            }
        }
    }
}
